
public class BlockNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Block";

  public BlockNode(Token token)
  {
    Token = token;
  }
}
