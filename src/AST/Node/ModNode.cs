
public class ModNode : NonLeafNode
{
  public override string Name { get; } = "Mod";
  public override int Line { get; }
  public override int Column { get; }

  public ModNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
