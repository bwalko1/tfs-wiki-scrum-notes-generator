using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using TWSNG.Custom_Exceptions;
using TWSNG.Interfaces;

namespace TWSNG {

  // TFS Wiki Scrum Notes Generator
  public class TWSNG {
    private const string SCRUM_NOTES_INDEX_LOCATION = "[Scrum Notes Index](/Teams/MRI-Affordable/Meeting-Notes-Hub/Scrum-Notes/Scrum-Notes-Index)";
    private const int ROW_INPUT_SIZE = 3;
    private const string OUTPUT_FILE_NAME = "TFSMarkdown";

    private readonly IMSRManager      _msrManager;
    private readonly IMarkdownManager _markdownManager;
    private readonly IUserIO          _userIo;

    public TWSNG(IMSRManager msrManager, IMarkdownManager markdownManager, IUserIO userIo) {
      _msrManager      = msrManager;
      _markdownManager = markdownManager;
      _userIo          = userIo;
    }

    public void Run() {
      var date            = CollectDate();
      var swimLanes       = CollectSwimLanes();
      var teamwideUpdates = CollectTeamwideUpdates();
      var actionItems     = CollectActionItems();
      var footer          = CollectFooter(date);

      Finalize(date, swimLanes, teamwideUpdates, actionItems, footer);
    }

    public DateTime CollectDate() {
      while (true) {
        try {
          WriteLine("Would you like to use today's date? (Y/N)");
          var input = GetUserInputTrimUpper();

          CheckInput(input);

          // business logic
          switch (input[0]) {
            case 'Y':
              Clear();
              return DateTime.Today;
            case 'N':
              return GetDateFromUser();
            default:
              throw new InvalidInputException("Invalid Input: Please use 'Y' or 'N' only.");
          }
        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public DateTime GetDateFromUser() {
      while (true) {
        try {
          WriteLine("What date would you like to use? (mm/dd/yyyy)");
          var dateInput = GetUserInputTrim();

          // business logic
          if (!DateTime.TryParseExact(dateInput, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime date)) {
            throw new InvalidInputException("Invalid Input: Incorrect date format.");
          }

          Clear();
          return date;
        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public List<ISwimLane> CollectSwimLanes() {
      var swimLanes = new List<ISwimLane>();
      while (true) {
        try {
          WriteLine("What is the name of the swim lane?");
          var swimLaneNameInput = GetUserInputTrim();

          if (swimLaneNameInput == "") {
            throw new InvalidInputException("Invalid Input: A swim lane should have a name.");
          }

          var table = CollectTable(swimLaneNameInput);
          // business logic?
          swimLanes.Add(_msrManager.CreateSwimLane(swimLaneNameInput, table));

          ClearAndWriteLine("Is there another swim lane? (Y/N)");
          var input = GetUserInputTrimUpper();

          CheckInput(input);

          // business logic
          switch (input[0]) {
            case 'Y':
              Clear();
              break;
            case 'N':
              Clear();
              return swimLanes;
            default:
              throw new InvalidInputException("Invalid Input: Please use 'Y' or 'N' only.");
          }

        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public ITable CollectTable(string swimLaneName) {
      ClearAndWriteLine($"The current swim lane is {swimLaneName}\n");

      var row = CollectRow();
      var rows = new List<IRow> { row };

      while (true) {
        try {
          ClearAndWriteLine($"The current swim lane is {swimLaneName}\n");

          row = CollectRow(rows[rows.Count - 1]);

          if (row.CardNumber == -1) {
            return _msrManager.CreateTable(rows);
          }

          rows.Add(row);
        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public IRow CollectRow(IRow lastRow = null) {
      while (true) {
        try {
          if (lastRow != null) {
            WriteLine($"Last entry: {lastRow.CardNumber} {lastRow.TeamMember} {lastRow.Update}");
          }

          if (lastRow == null) {
            WriteLine("Enter the update ([Card number or 0] [Name] [Update])");
          } else {
            WriteLine("Enter the update ([Card number or 0] [Name] [Update] or 'E' to exit)");
          }

          var rowInput = GetUserInputTrim().Split(' ', ROW_INPUT_SIZE);
          var cardNumberInput = rowInput[0];

          if (cardNumberInput.ToUpper() == "E") {
            return _msrManager.CreateEmptyRow();
          }

          if (!IsRowInputSizeValid(rowInput)) {
            throw new InvalidInputException("Invalid Input: The input does not contain everything it needs.");
          }

          if (!IsNumber(cardNumberInput)) {
            throw new InvalidInputException("Invalid Input: That is not a number.");
          }

          var cardNumber = int.Parse(cardNumberInput);

          var teamMember = rowInput[1];
          var update = rowInput[2];

          // business logic
          return _msrManager.CreateRow(cardNumber, teamMember, update);
        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public List<string> CollectTeamwideUpdates() {
      var teamwideUpdates = new List<string>();

      while (true) {
        try {
          if (teamwideUpdates.Count == 0) {
            WriteLine("Are there any team-wide updates? (Y/N)");
          } else {
            ClearAndWriteLine("Is there another team-wide update? (Y/N)");
          }
          var input = GetUserInputTrimUpper();

          CheckInput(input);

          // business logic
          switch (input[0]) {
            case 'Y':
              Clear();
              if (teamwideUpdates.Count > 0) {
                WriteLine($"Last entry: {teamwideUpdates[teamwideUpdates.Count - 1]}");
              }

              WriteLine("What is the update?");
              teamwideUpdates.Add(GetUserInputTrim());
              break;
            case 'N':
              Clear();
              return teamwideUpdates;
            default:
              throw new InvalidInputException("Invalid Input: Please use 'Y' or 'N' only.");
          }
        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public List<string> CollectActionItems() {
      var actionItems = new List<string>();
      while (true) {
        try {
          if (actionItems.Count == 0) {
            WriteLine("Are there any action items? (Y/N)");
          } else {
            WriteLine("Is there another action item? (Y/N)");
          }
          var input = GetUserInputTrimUpper();

          CheckInput(input);

          switch (input[0]) {
            case 'Y':
              ClearAndWriteLine("What is the action item?");
              actionItems.Add(GetUserInputTrim());
              break;
            case 'N':
              Clear();
              return actionItems;
            default:
              throw new InvalidInputException("Invalid Input: Please use 'Y' or 'N' only.");
          }
        } catch (InvalidInputException invalidInputException) {
          ClearAndWriteLine(invalidInputException.Message);
        }
      }
    }

    public IFooter CollectFooter(DateTime date) {
      return _msrManager.CreateFooter(date, SCRUM_NOTES_INDEX_LOCATION);
    }

    public void Finalize(DateTime date, List<ISwimLane> swimLanes, List<string> teamwideUpdates, List<string> actionItems, IFooter footer) {
      var markdown = "";
      try {
        var header = _msrManager.CreateHeader(date, actionItems, teamwideUpdates);
        var MSR = _msrManager.CreateMasterScrumRecord(header, swimLanes, footer);

        markdown = _markdownManager.TranspileMarkdown(MSR);
        _markdownManager.CopyMarkdown(markdown);

        if (_markdownManager.FileExists(OUTPUT_FILE_NAME)) {
          _markdownManager.DeleteFile(OUTPUT_FILE_NAME);
        }

        ClearAndWriteLine("Markdown copied to clipboard! Press 'Enter' key to exit.");
        _userIo.GetUserInput();
      } catch {
        _markdownManager.ExportToFile(OUTPUT_FILE_NAME, markdown);
        ClearAndWriteLine("There was an error copying to the clipboard, but your markdown has been output to 'TFSMarkdown.txt'.");
        _userIo.GetUserInput();
      }
    }

    public static bool IsNumber(string input) {
      return Regex.IsMatch(input, @"^\d+$");
    }

    public void Clear() {
      _userIo.Clear();
    }

    public void WriteLine(string input) {
      _userIo.WriteLine(input);
    }

    public void ClearAndWriteLine(string input) {
      _userIo.ClearAndWriteLine(input);
    }

    public string GetUserInputTrim() {
      return _userIo.GetUserInput().Trim();
    }

    public string GetUserInputTrimUpper() {
      return _userIo.GetUserInput().Trim().ToUpper();
    }

    public bool IsRowInputSizeValid(string[] rowInput) {
      return rowInput.Length == ROW_INPUT_SIZE;
    }

    public void CheckInput(string input) {
      if (input == "") {
        throw new InvalidInputException("Invalid Input: There was no input.");
      }
    }
  }
}
