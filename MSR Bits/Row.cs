using System.Collections.Generic;

namespace TWSNG.MSR_Bits {
  public class Row : IRow {
    public int    CardNumber { get; set; }
    public string TeamMember { get; set; }
    public string Update     { get; set; }

    public Row(int cardNumber, string teamMember, string update) {
      CardNumber = cardNumber;
      TeamMember = teamMember;
      Update     = update;
    }
  }
}
