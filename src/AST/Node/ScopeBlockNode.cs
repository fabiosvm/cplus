
public class ScopeBlockNode : NonLeafNode
{
  public override string Name { get; } = "ScopeBlock";
  public override int Line { get; }
  public override int Column { get; }

  public ScopeBlockNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
