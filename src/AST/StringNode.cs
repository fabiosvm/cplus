
public class StringNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "String";

  public StringNode(Token token)
  {
    Token = token;
  }
}
