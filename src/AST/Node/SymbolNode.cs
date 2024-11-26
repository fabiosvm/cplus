
public class SymbolNode : LiteralNode
{
  public override string Name { get; } = "Symbol";
  public override int Line { get; }
  public override int Column { get; }
  public override string Lexeme { get; }

  public SymbolNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
    Lexeme = token.Lexeme;
  }
}
