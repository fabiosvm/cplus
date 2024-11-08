
public class LexerState
{
  public int Pos { get; }
  public int Line { get; }
  public int Column { get; }
  public Token CurrentToken { get; }

  public LexerState(int pos, int line, int column, Token currentToken)
  {
    Pos = pos;
    Line = line;
    Column = column;
    CurrentToken = currentToken;
  }
}
