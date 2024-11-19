
public class BoolTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "BoolType";

  public BoolTypeNode(Token token)
  {
    Token = token;
  }
}
