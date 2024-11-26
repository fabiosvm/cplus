
public class CharTypeNode : LeafNode
{
  public override string Name { get; } = "CharType";
  public override int Line { get; }
  public override int Column { get; }

  public CharTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
