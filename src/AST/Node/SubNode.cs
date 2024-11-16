
public class SubNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Sub";

  public SubNode(Token token)
  {
    Token = token;
  }
}
