
public class OrNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Or";

  public OrNode(Token token)
  {
    Token = token;
  }
}
