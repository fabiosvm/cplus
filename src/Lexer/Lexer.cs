
public class Lexer
{
  public string Source { get; }
  public Diagnostics Diagnostics { get; } = new Diagnostics();
  private int pos;
  private int line;
  private int column;
  public Token CurrentToken { get; private set; }

  public Lexer(string source)
  {
    Source = source;
    pos = 0;
    line = 1;
    column = 1;
    CurrentToken = Token.Invalid;
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
    if (matchChar('[', TokenKind.LBracket)) return;
    if (matchChar(']', TokenKind.RBracket)) return;
    if (matchChar('{', TokenKind.LBrace)) return;
    if (matchChar('}', TokenKind.RBrace)) return;
    if (matchChars("||", TokenKind.PipePipe)) return;
    if (matchChars("&&", TokenKind.AmpAmp)) return;
    if (matchChars("==", TokenKind.EqEq)) return;
    if (matchChar('=', TokenKind.Eq)) return;
    if (matchChars("<=", TokenKind.LtEq)) return;
    if (matchChar('<', TokenKind.Lt)) return;
    if (matchChars(">=", TokenKind.GtEq)) return;
    if (matchChar('>', TokenKind.Gt)) return;
    if (matchChars("!=", TokenKind.NotEq)) return;
    if (matchChar('!', TokenKind.Not)) return;
    if (matchChar('+', TokenKind.Plus)) return;
    if (matchChar('-', TokenKind.Minus)) return;
    if (matchChar('*', TokenKind.Star)) return;
    if (matchChar('/', TokenKind.Slash)) return;
    if (matchChar('%', TokenKind.Percent)) return;
    if (matchNumberLiteral()) return;
    if (matchCharLiteral()) return;
    if (matchStringLiteral()) return;
    if (matchKeyword("break", TokenKind.BreakKW)) return;
    if (matchKeyword("char", TokenKind.CharKW)) return;
    if (matchKeyword("continue", TokenKind.ContinueKW)) return;
    if (matchKeyword("do", TokenKind.DoKW)) return;
    if (matchKeyword("double", TokenKind.DoubleKW)) return;
    if (matchKeyword("else", TokenKind.ElseKW)) return;
    if (matchKeyword("float", TokenKind.FloatKW)) return;
    if (matchKeyword("func", TokenKind.FuncKW)) return;
    if (matchKeyword("if", TokenKind.IfKW)) return;
    if (matchKeyword("int", TokenKind.IntKW)) return;
    if (matchKeyword("long", TokenKind.LongKW)) return;
    if (matchKeyword("return", TokenKind.ReturnKW)) return;
    if (matchKeyword("short", TokenKind.ShortKW)) return;
    if (matchKeyword("String", TokenKind.StringKW)) return;
    if (matchKeyword("var", TokenKind.VarKW)) return;
    if (matchKeyword("void", TokenKind.VoidKW)) return;
    if (matchKeyword("while", TokenKind.WhileKW)) return;
    if (matchIdent()) return;

    Diagnostics.Report(MessageKind.Fatal, $"Unexpected character '{currentChar}' [{line}:{column}]");
  }

  public bool Match(TokenKind kind) => CurrentToken.Kind == kind;

  public LexerState Save() => new LexerState(pos, line, column, CurrentToken);

  public void Restore(LexerState state)
  {
    pos = state.Pos;
    line = state.Line;
    column = state.Column;
    CurrentToken = state.CurrentToken;
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
    for (var i = 0; i < n; i++)
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

  private bool matchChars(string chars, TokenKind kind)
  {
    var length = chars.Length;
    if (pos + length >= Source.Length)
      return false;

    if (Source.Substring(pos, length) != chars)
      return false;

    CurrentToken = new Token(kind, line, column, chars);
    nextChars(length);
    return true;
  }

  private bool matchKeyword(string keyword, TokenKind kind)
  {
    var length = keyword.Length;
    if (pos + length >= Source.Length)
      return false;

    if (Source.Substring(pos, length) != keyword)
      return false;

    var c = charAt(length);
    if (char.IsLetterOrDigit(c) || c == '_')
      return false;

    CurrentToken = new Token(kind, line, column, keyword);
    nextChars(length);
    return true;
  }

  private bool matchNumberLiteral()
  {
    TokenKind kind = TokenKind.IntLiteral;
    if (!char.IsDigit(currentChar()))
      return false;

    var length = 1;
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

  private bool matchCharLiteral()
  {
    if (currentChar() != '\'')
      return false;

    if (charAt(2) != '\'')
    {
      Diagnostics.Report(MessageKind.Fatal, $"Invalid character literal [{line}:{column}]");
      return false;
    }

    CurrentToken = new Token(TokenKind.CharLiteral, line, column, charAt(1).ToString());
    nextChars(3);
    return true;
  }

  private bool matchStringLiteral()
  {
    if (currentChar() != '"')
      return false;

    var length = 1;
    while (charAt(length) != '"')
    {
      if (charAt(length) == '\0')
      {
        Diagnostics.Report(MessageKind.Fatal, $"Unterminated string [{line}:{column}]");
        return false;
      }
      length++;
    }

    CurrentToken = new Token(TokenKind.StringLiteral, line, column, Source.Substring(pos, length - 1));
    nextChars(length + 1);
    return true;
  }

  private bool matchIdent()
  {
    if (currentChar() != '_' && !char.IsLetter(currentChar()))
      return false;

    var length = 1;
    while (charAt(length) == '_' || char.IsLetterOrDigit(charAt(length)))
      length++;

    CurrentToken = new Token(TokenKind.Ident, line, column, Source.Substring(pos, length));
    nextChars(length);
    return true;
  }
}
