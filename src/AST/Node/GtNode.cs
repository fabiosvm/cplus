
public class GtNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Gt";

  public GtNode(Token token)
  {
    Token = token;
  }
}
