
public class BreakNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Break";

  public BreakNode(Token token)
  {
    Token = token;
  }
}
