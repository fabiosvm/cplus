
public class NewNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "New";

  public NewNode(Token token)
  {
    Token = token;
  }
}
