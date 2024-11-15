
public class InvalidNode : Node
{
  public bool IsLeaf { get; } = false;
  public string Name { get; } = string.Empty;
  public Token Token { get; } = new Token(TokenKind.Invalid, -1, -1, string.Empty);

  public void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}<invalid>");
  }
}
