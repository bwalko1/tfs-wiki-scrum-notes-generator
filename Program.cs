using System;

namespace TWSNG {

  // Version 3.1
  // Last updated: 1/16/20
  // Written by: Ben Walko

  public class Program {

    [STAThread]
    private static void Main() {
      var msrManager      = new MSRManager();
      var markdownManager = new MarkdownManager();
      var userIo          = new ConsoleUserIO();
      var TWSNG           = new TWSNG(msrManager, markdownManager, userIo);

      TWSNG.Run();
    }
  }
}
