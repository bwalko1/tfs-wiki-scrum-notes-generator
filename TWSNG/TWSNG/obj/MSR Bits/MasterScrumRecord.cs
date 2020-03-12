using System.Collections.Generic;
using TWSNG.Interfaces;

namespace TWSNG {
  class MasterScrumRecord : IMasterScrumRecord {
    public IHeader         Header    { get; set; }
    public List<ISwimLane> SwimLanes { get; set; }
    public IFooter         Footer    { get; set; }

    public MasterScrumRecord() { }
    public MasterScrumRecord(IHeader header, List<ISwimLane> swimLanes, IFooter footer) {
      Header    = header;
      SwimLanes = swimLanes;
      Footer    = footer;
    }
  }
}
