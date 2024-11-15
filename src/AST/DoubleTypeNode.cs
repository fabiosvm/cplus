
public class DoubleTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "DoubleType";

  public DoubleTypeNode(Token token)
  {
    Token = token;
  }
}
