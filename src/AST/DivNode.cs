
public class DivNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Div";

  public DivNode(Token token)
  {
    Token = token;
  }
}
