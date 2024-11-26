
public class Token
{
  public static readonly Token None = new Token(TokenKind.None, -1, -1, string.Empty);

  public TokenKind Kind;
  public int Line { get; }
  public int Column { get; }
  public string Lexeme { get; }

  public Token(TokenKind kind, int line, int column, string lexeme)
  {
    Kind = kind;
    Line = line;
    Column = column;
    Lexeme = lexeme;
  }

  public override string ToString()
  {
    if (Kind == TokenKind.IntLiteral
     || Kind == TokenKind.FloatLiteral
     || Kind == TokenKind.CharLiteral
     || Kind == TokenKind.StringLiteral
     || Kind == TokenKind.Ident)
    {
      return $"{Kind}({Lexeme})";
    }
    return Kind.ToString();
  }
}
