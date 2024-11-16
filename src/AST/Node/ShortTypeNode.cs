
public class ShortTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "ShortType";

  public ShortTypeNode(Token token)
  {
    Token = token;
  }
}
