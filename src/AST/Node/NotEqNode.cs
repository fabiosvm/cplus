
public class NeNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Ne";

  public NeNode(Token token)
  {
    Token = token;
  }
}
