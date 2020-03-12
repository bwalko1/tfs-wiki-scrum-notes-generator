using System;
using System.Collections.Generic;

namespace TWSNG.Interfaces {
  public interface IMSRManager {
    IMasterScrumRecord CreateMasterScrumRecord (IHeader header, List<ISwimLane> swimLanes, IFooter footer);
    IHeader            CreateHeader            (DateTime date, List<string> actionItems, List<string> teamwideUpdates);
    ISwimLane          CreateSwimLane          (string name, ITable table);
    ITable             CreateTable             (List<IRow> rows);
    IRow               CreateRow               (int cardNumbers, string teamMember, string update);
    IFooter            CreateFooter            (DateTime date, string scrumNotesLocation);
    IRow               CreateEmptyRow          ();

  }
}
