
public class InitListNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "InitList";

  public InitListNode(Token token)
  {
    Token = token;
  }
}
