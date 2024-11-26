
public class FloatLiteralNode : LiteralNode
{
  public override string Name { get; } = "FloatLiteral";
  public override int Line { get; }
  public override int Column { get; }
  public override string Lexeme { get; }

  public FloatLiteralNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
    Lexeme = token.Lexeme;
  }
}
