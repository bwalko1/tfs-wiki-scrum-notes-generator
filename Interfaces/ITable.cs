using System.Collections.Generic;

namespace TWSNG.Interfaces {
  public interface ITable {
    List<IRow> Rows { get; set; }
  }
}
