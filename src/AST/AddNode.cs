
public class AddNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Add";

  public AddNode(Token token)
  {
    Token = token;
  }
}
