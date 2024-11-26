
public class StringLiteralNode : LiteralNode
{
  public override string Name { get; } = "StringLiteral";
  public override int Line { get; }
  public override int Column { get; }
  public override string Lexeme { get; }

  public StringLiteralNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
    Lexeme = token.Lexeme;
  }
}
