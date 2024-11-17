
abstract public class LeafNode : Node
{
  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}");
  }
}
