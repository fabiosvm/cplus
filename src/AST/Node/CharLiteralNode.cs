
public class CharLiteralNode : LiteralNode
{
  public override string Name { get; } = "CharLiteral";
  public override int Line { get; }
  public override int Column { get; }
  public override string Lexeme { get; }

  public CharLiteralNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
    Lexeme = token.Lexeme;
  }
}
