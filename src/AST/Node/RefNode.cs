
public class RefNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Ref";

  public RefNode(Token token)
  {
    Token = token;
  }
}
