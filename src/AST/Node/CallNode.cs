
public class CallNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Call";

  public CallNode(Token token)
  {
    Token = token;
  }
}
