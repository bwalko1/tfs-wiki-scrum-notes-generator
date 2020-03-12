using System.Collections.Generic;

namespace TWSNG.Interfaces {
  public interface IMasterScrumRecord {
    IHeader         Header    { get; set; }
    List<ISwimLane> SwimLanes { get; set; }
    IFooter         Footer    { get; set; }
  }
}
