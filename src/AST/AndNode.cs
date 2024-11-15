
public class AndNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "And";

  public AndNode(Token token)
  {
    Token = token;
  }
}
