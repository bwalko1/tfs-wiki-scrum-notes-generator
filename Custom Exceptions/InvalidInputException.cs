using System;

namespace TWSNG.Custom_Exceptions {
  class InvalidInputException : Exception {
    public InvalidInputException(string message) : base(message) { }
  }
}
