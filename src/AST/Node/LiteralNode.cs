
abstract public class LiteralNode : LeafNode
{
  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}: '{Token.Lexeme}'");
  }
}
