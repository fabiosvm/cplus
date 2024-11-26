
public class BlockNode : NonLeafNode
{
  public override string Name { get; } = "Block";
  public override int Line { get; }
  public override int Column { get; }

  public BlockNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
