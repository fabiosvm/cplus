
public class NotEqNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "NotEq";

  public NotEqNode(Token token)
  {
    Token = token;
  }
}
