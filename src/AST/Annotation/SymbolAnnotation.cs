
using System.Diagnostics;

public class SymbolAnnotation : Annotation
{
  public override string Name => throw new UnreachableException();
  public Symbol Symbol { get; }

  public SymbolAnnotation(Symbol symbol)
  {
    Symbol = symbol;
  }

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}name='{Symbol.Name}'");
    Console.WriteLine($"{new string(' ', depth * 2)}kind={Symbol.Kind}");
    Console.WriteLine($"{new string(' ', depth * 2)}index={Symbol.Index}");
  }
}
