
using System.Diagnostics;

public class NoneNode : Node
{
  public override string Name { get; } = "None";
  public override int Line => throw new UnreachableException();
  public override int Column => throw new UnreachableException();
}
