using System;
using System.Collections.Generic;

namespace TWSNG {
  public interface IHeader {
    DateTime     Date            { get; set; }
    List<string> ActionItems     { get; set; }
    List<string> TeamwideUpdates { get; set; }
  }
}
