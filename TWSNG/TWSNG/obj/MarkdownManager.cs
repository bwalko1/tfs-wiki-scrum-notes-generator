using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TWSNG.Interfaces;

namespace TWSNG {
  public class MarkdownManager : IMarkdownManager {
    public string TranspileMarkdown(IMasterScrumRecord MSR) {
      var markdown = "";

      markdown += TranspileDate            (MSR.Header);
      markdown += TranspileActionItems     (MSR.Header);
      markdown += TranspileTeamwideUpdates (MSR.Header);
      markdown += TranspileSwimLanes       (MSR.SwimLanes);
      markdown += TranspileFooter          (MSR.Footer);

      return markdown;
    }

    public string TranspileDate(IHeader header) {
      return $"## {header.Date.ToString("D")}\n";
    }

    public string TranspileActionItems(IHeader header) {
      var markdown = "";
      markdown += "## Action Items\n";

      if (header.ActionItems.Count > 0) {
        foreach (var actionItem in header.ActionItems) {
          markdown += $"- [ ] {actionItem}\n";
        }
      } else {
        markdown += "###### No action items\n";
      }

      return markdown;
    }

    public string TranspileTeamwideUpdates(IHeader header) {
      var markdown = "";
      markdown += "## Team-wide Updates\n";

      if (header.TeamwideUpdates.Count > 0) {
        foreach (var teamwideUpdate in header.TeamwideUpdates) {
          markdown += $"- {teamwideUpdate}\n";
        }
      } else {
        markdown += "###### No team-wide updates\n";
      }

        return markdown;
    }

    public string TranspileSwimLanes(List<ISwimLane> swimLanes) {
      var markdown = "";

      foreach (var swimLane in swimLanes) {
        markdown += $"### <u>{swimLane.Name}</u>\n" +
                     "| Card(s) | Update | Team Member |\n" +
                     "|-----------|-----------|:-----------:|\n";
        markdown += TranspileTable(swimLane.Table);
      }

      return markdown;
    }

    public string TranspileTable(ITable table) {
      var markdown = "";

      foreach (var row in table.Rows) {
        var cardNumber = "";

        if (row.CardNumber == 0) {
          cardNumber = "General Update";
        } else {
          cardNumber = TranspileCardNumber(row.CardNumber);
        }

        markdown += $"| {cardNumber} | {row.Update} | {row.TeamMember} |\n";
      }

      return markdown;
    }

    public string TranspileCardNumber(int cardNumber) {
      return $"<br> #{cardNumber} </br>";
    }

    public string TranspileFooter(IFooter footer) {
      var dateIndexLocation = $"[{footer.Date.ToString("MMMM")}](/Teams/MRI-Affordable/Meeting-Notes-Hub/Scrum-Notes/Scrum-Notes-Index/{footer.Date.Year}/{footer.Date.ToString("MMMM")})";

      return "\n</br> \n\n" +
             $"### Back to {dateIndexLocation}\n" +
             $"### Back to {footer.ScrumNotesLocation}";
    }

    public void CopyMarkdown(string markdown) {
      Clipboard.SetText(markdown);
    }

    public void ExportToFile(string fileName, string markdown) {
      File.WriteAllText($"{fileName}.txt", markdown);
    }

    public void DeleteFile(string fileName) {
      File.Delete($"{fileName}.txt");
    }

    public bool FileExists(string fileName) {
      return File.Exists($"{fileName}.txt");
    }
  }
}
