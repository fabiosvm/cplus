
public class UnaryPlusNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "UnaryPlus";

  public UnaryPlusNode(Token token)
  {
    Token = token;
  }
}
