
public class IdentNode : LiteralNode
{
  public override string Name { get; } = "Ident";
  public override int Line { get; }
  public override int Column { get; }
  public override string Lexeme { get; }

  public IdentNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
    Lexeme = token.Lexeme;
  }
}
