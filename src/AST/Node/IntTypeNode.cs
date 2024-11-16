
public class IntTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "IntType";

  public IntTypeNode(Token token)
  {
    Token = token;
  }
}
