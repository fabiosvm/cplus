
public class NotNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Not";

  public NotNode(Token token)
  {
    Token = token;
  }
}
