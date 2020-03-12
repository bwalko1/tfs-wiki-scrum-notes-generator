using System;
using System.Collections.Generic;

namespace TWSNG.MSR_Bits {
  public class Header : IHeader {
    public DateTime     Date            { get; set; }
    public List<string> ActionItems     { get; set; }
    public List<string> TeamwideUpdates { get; set; }

    public Header(DateTime date, List<string> actionItems, List<string> teamwideUpdates) {
      Date            = date;
      ActionItems     = actionItems;
      TeamwideUpdates = teamwideUpdates;
    }
  }
}
