
public class ModuleNode : NonLeafNode
{
  public override string Name { get; } = "Module";
  public override int Line => throw new NotImplementedException();
  public override int Column => throw new NotImplementedException();
}
