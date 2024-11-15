
public class IntNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Int";

  public IntNode(Token token)
  {
    Token = token;
  }
}
