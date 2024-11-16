
public class NoneNode : Node
{
  public override Token Token { get; } = new Token(TokenKind.None, -1, -1, string.Empty);
  public override bool IsLeaf { get; } = false;
  public override string Name { get; } = string.Empty;

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}<none>");
  }
}
