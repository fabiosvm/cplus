
public class ArrayNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Array";

  public ArrayNode(Token token)
  {
    Token = token;
  }
}
