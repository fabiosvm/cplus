
public class BreakNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Break";

  public BreakNode(Token token)
  {
    Token = token;
  }
}