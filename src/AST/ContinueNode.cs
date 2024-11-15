
public class ContinueNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Continue";

  public ContinueNode(Token token)
  {
    Token = token;
  }
}
