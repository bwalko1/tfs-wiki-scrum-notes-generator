using System;
using System.Collections.Generic;
using TWSNG.Interfaces;
using TWSNG.MSR_Bits;

namespace TWSNG {
  public class MSRManager : IMSRManager {
    public IMasterScrumRecord CreateMasterScrumRecord(IHeader header, List<ISwimLane> swimLanes, IFooter footer) {
      return new MasterScrumRecord(header, swimLanes, footer);
    }

    public IHeader CreateHeader(DateTime date, List<string> actionItems, List<string> teamwideUpdates) {
      return new Header(date, actionItems, teamwideUpdates);
    }

    public ISwimLane CreateSwimLane(string name, ITable table) {
      return new SwimLane(name, table);
    }

    public ITable CreateTable(List<IRow> rows) {
      return new Table(rows);
    }

    public IRow CreateRow(int cardNumber, string teamMember, string update) {
      return new Row(cardNumber, teamMember, update);
    }

    public IFooter CreateFooter(DateTime date, string scrumNotesLocation) {
      return new Footer(date, scrumNotesLocation);
    }

    public IRow CreateEmptyRow() {
      return new Row(-1, "", "");
    }
  }
}
