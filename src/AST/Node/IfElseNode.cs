
public class IfElseNode : NonLeafNode
{
  public override string Name { get; } = "IfElse";
  public override int Line { get; }
  public override int Column { get; }

  public IfElseNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
