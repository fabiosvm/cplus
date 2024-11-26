
public class ReturnNode : NonLeafNode
{
  public override string Name { get; } = "Return";
  public override int Line { get; }
  public override int Column { get; }

  public ReturnNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
