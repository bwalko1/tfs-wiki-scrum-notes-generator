using System.Collections.Generic;
using TWSNG.Interfaces;

namespace TWSNG.MSR_Bits {
  public class Table : ITable {
    public List<IRow> Rows { get; set; }

    public Table(List<IRow> rows) {
      Rows = rows;
    }
  }
}
