
public class FuncDeclNode : NonLeafNode
{
  public override string Name { get; } = "FuncDecl";
  public override int Line { get; }
  public override int Column { get; }

  public FuncDeclNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}
