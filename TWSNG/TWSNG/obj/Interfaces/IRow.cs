using System.Collections.Generic;

namespace TWSNG {
  public interface IRow {
    int    CardNumber { get; set; }
    string TeamMember { get; set; }
    string Update     { get; set; }
  }
}
