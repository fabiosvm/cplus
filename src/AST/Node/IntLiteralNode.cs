
public class IntLiteralNode : LiteralNode
{
  public override string Name { get; } = "IntLiteral";
  public override int Line { get; }
  public override int Column { get; }
  public override string Lexeme { get; }

  public IntLiteralNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
    Lexeme = token.Lexeme;
  }
}
