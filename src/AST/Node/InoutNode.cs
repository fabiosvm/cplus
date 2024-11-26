
public class InoutNode : LeafNode
{
  public override string Name { get; } = "Inout";
  public override int Line { get; }
  public override int Column { get; }

  public InoutNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
