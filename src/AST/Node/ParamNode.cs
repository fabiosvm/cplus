
public class ParamNode : NonLeafNode
{
  public override string Name { get; } = "Param";
  public override int Line { get; }
  public override int Column { get; }

  public ParamNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
