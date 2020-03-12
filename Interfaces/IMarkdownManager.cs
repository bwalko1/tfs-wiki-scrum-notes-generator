namespace TWSNG.Interfaces {
  public interface IMarkdownManager {
    string TranspileMarkdown (IMasterScrumRecord MSR);
    void   CopyMarkdown      (string markdown);
    void   ExportToFile      (string fileName, string markdown);
    void   DeleteFile        (string fileName);
    bool   FileExists        (string fileName);
  }
}
