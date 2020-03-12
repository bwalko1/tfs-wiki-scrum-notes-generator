using TWSNG.Interfaces;

namespace TWSNG.MSR_Bits {
  public class SwimLane : ISwimLane {
    public string Name  { get; set; }
    public ITable Table { get; set; }

    public SwimLane(string name, ITable table) {
      Name  = name;
      Table = table;
    }
  }
}
