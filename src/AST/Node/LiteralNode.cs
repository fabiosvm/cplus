
abstract public class LiteralNode : LeafNode
{
  abstract public string Lexeme { get; }

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}('{Lexeme}')");
  }
}
