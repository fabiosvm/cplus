
public class Lexer
{
  public string Source { get; }
  public Diagnostics Diagnostics { get; }
  private int pos;
  private int line;
  private int column;
  public Token CurrentToken { get; private set; }

  public Lexer(string source, Diagnostics diagnostics)
  {
    Source = source;
    Diagnostics = diagnostics;
    pos = 0;
    line = 1;
    column = 1;
    CurrentToken = new Token(TokenKind.Invalid, -1, -1, string.Empty);
    NextToken();
  }

  public void NextToken()
  {
    skipWhiteSpace();
    if (matchChar('\0', TokenKind.Eof)) return;
    if (matchChar(',', TokenKind.Comma)) return;
    if (matchChar(';', TokenKind.Semicolon)) return;
    if (matchChar('(', TokenKind.LParen)) return;
    if (matchChar(')', TokenKind.RParen)) return;
    if (matchChar('{', TokenKind.LBrace)) return;
    if (matchChar('}', TokenKind.RBrace)) return;
    if (matchChar('+', TokenKind.Plus)) return;
    if (matchChar('-', TokenKind.Minus)) return;
    if (matchChar('*', TokenKind.Star)) return;
    if (matchChar('/', TokenKind.Slash)) return;
    if (matchChar('%', TokenKind.Percent)) return;
    if (matchNumber()) return;
    if (matchKeyword("float", TokenKind.FloatKW)) return;
    if (matchKeyword("int", TokenKind.IntKW)) return;
    if (matchKeyword("return", TokenKind.ReturnKW)) return;
    if (matchKeyword("void", TokenKind.VoidKW)) return;
    if (matchIdent()) return;
    Diagnostics.Report(MessageKind.Fatal, $"Unexpected character '{currentChar}' at {line}:{column}");
  }

  private char charAt(int offset)
  {
    if (pos + offset >= Source.Length)
      return '\0';
    return Source[pos + offset];
  } 

  private char currentChar() => charAt(0);

  private void skipWhiteSpace()
  {
    while (char.IsWhiteSpace(currentChar()))
      nextChar();
  }

  private void nextChar()
  {
    if (currentChar() == '\n')
    {
      line++;
      column = 1;
      pos++;
      return;
    }
    column++;
    pos++;
  }

  private void nextChars(int n)
  {
    for (int i = 0; i < n; i++)
      nextChar();
  }

  private bool matchChar(char c, TokenKind kind)
  {
    if (currentChar() != c)
      return false;
    CurrentToken = new Token(kind, line, column, c.ToString());
    nextChar();
    return true;
  }

  private bool matchKeyword(string keyword, TokenKind kind)
  {
    int length = keyword.Length;
    if (Source.Substring(pos, length) != keyword
      || (pos + length < Source.Length && char.IsLetterOrDigit(Source[pos + length]))
      || (pos + length < Source.Length && Source[pos + length] == '_'))
      return false;
    CurrentToken = new Token(kind, line, column, keyword);
    nextChars(length);
    return true;
  }

  private bool matchNumber()
  {
    TokenKind kind = TokenKind.IntLiteral;
    if (!char.IsDigit(currentChar()))
      return false;
    int length = 1;
    while (char.IsDigit(charAt(length)))
      length++;
    if (charAt(length) == '.')
    {
      kind = TokenKind.FloatLiteral;
      length++;
      while (char.IsDigit(charAt(length)))
        length++;
    }
    CurrentToken = new Token(kind, line, column, Source.Substring(pos, length));
    nextChars(length);
    return true;
  }

  private bool matchIdent()
  {
    if (currentChar() != '_' && !char.IsLetter(currentChar()))
      return false;
    int length = 1;
    while (charAt(length) == '_' || char.IsLetterOrDigit(charAt(length)))
      length++;
    CurrentToken = new Token(TokenKind.Ident, line, column, Source.Substring(pos, length));
    nextChars(length);
    return true;
  }
}
