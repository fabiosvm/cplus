
using System.Diagnostics;

public class ModuleNode : NonLeafNode
{
  public override string Name { get; } = "Module";
  public override int Line => throw new UnreachableException();
  public override int Column => throw new UnreachableException();
}
