
public class FloatTypeNode : LeafNode
{
  public override string Name { get; } = "FloatType";
  public override int Line { get; }
  public override int Column { get; }

  public FloatTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
