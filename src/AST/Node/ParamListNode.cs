
public class ParamListNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "ParamList";

  public ParamListNode(Token token)
  {
    Token = token;
  }
}
