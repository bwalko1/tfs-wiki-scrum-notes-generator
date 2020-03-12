using System;
using TWSNG.Interfaces;

namespace TWSNG {
  public class ConsoleUserIO : IUserIO {
    public void WriteLine(string message) {
      Console.WriteLine(message);
    }

    public void Write(string message) {
      Console.Write(message);
    }

    public string ReadLine() {
      return Console.ReadLine();
    }

    public void Clear() {
      Console.Clear();
    }

    public string GetUserInput() {
      Write(": ");
      return ReadLine();
    }

    public void ClearAndWriteLine(string message) {
      Clear();
      WriteLine(message);
    }
  }
}
