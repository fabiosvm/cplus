
public class NoneNode : Node
{
  public override Token Token { get; } = Token.None;
  public override string Name { get; } = string.Empty;

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}<none>");
  }
}
