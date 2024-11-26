
public class RefNode : NonLeafNode
{
  public override string Name { get; } = "Ref";
  public override int Line { get; }
  public override int Column { get; }

  public RefNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
