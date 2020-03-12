using System;
using TWSNG.Interfaces;

namespace TWSNG.MSR_Bits {
  public class Footer : IFooter {
    public DateTime Date               { get; set; }
    public string   ScrumNotesLocation { get; set; }

    public Footer(DateTime date, string scrumNotesLocation) {
      Date               = date;
      ScrumNotesLocation = scrumNotesLocation;
    }
  }
}
