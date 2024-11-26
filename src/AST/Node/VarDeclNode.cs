
public class VarDeclNode : NonLeafNode
{
  public override string Name { get; } = "VarDecl";
  public override int Line { get; }
  public override int Column { get; }

  public VarDeclNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
