
public class ForNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "For";

  public ForNode(Token token)
  {
    Token = token;
  }
}
