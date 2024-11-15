
public class ModuleNode : NonLeafNode
{
  public override Token Token { get; } = Token.Invalid;
  public override string Name { get; } = "Module";
}
