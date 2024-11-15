
public class LtNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Lt";

  public LtNode(Token token)
  {
    Token = token;
  }
}
