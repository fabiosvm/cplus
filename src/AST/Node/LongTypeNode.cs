
public class LongTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "LongType";

  public LongTypeNode(Token token)
  {
    Token = token;
  }
}
