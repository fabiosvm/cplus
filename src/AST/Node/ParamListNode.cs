
public class ParamListNode : NonLeafNode
{
  public override string Name { get; } = "ParamList";
  public override int Line { get; }
  public override int Column { get; }

  public ParamListNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
