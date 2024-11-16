
public class ElementNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Element";

  public ElementNode(Token token)
  {
    Token = token;
  }
}
