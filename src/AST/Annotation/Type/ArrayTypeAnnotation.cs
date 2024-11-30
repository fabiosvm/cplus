
public class ArrayTypeAnnotation : TypeAnnotation
{
  public override string Name { get; } = "array";

  public TypeAnnotation ElementType { get; }

  public ArrayTypeAnnotation(TypeAnnotation elementType)
  {
    ElementType = elementType;
  }

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");
    ElementType.Print(depth + 1);
  }
}
