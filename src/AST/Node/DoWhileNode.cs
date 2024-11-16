
public class DoWhileNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "DoWhile";

  public DoWhileNode(Token token)
  {
    Token = token;
  }
}
