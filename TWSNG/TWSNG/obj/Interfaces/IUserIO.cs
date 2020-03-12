namespace TWSNG.Interfaces {
  public interface IUserIO {
    void   Write             (string message);
    void   Clear             ();
    void   WriteLine         (string message);
    void   ClearAndWriteLine (string message);
    string ReadLine          ();
    string GetUserInput      ();
  }
}
