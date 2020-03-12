using System;

namespace TWSNG.Interfaces {
  public interface IFooter {
    DateTime Date             { get; set; }
    string ScrumNotesLocation { get; set; }
  }
}
