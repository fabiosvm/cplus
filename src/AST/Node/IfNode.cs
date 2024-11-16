
public class IfNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "If";

  public IfNode(Token token)
  {
    Token = token;
  }
}
