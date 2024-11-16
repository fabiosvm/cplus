
public class WhileNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "While";

  public WhileNode(Token token)
  {
    Token = token;
  }
}
