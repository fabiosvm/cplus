
public class FunctionTypeAnnotation : TypeAnnotation
{
  public override string Name { get; } = "function";
  public TypeAnnotation ReturnType { get; }
  public List<ParamTypeAnnotation> ParameterTypes { get; } = new List<ParamTypeAnnotation>();

  public FunctionTypeAnnotation(TypeAnnotation returnType)
  {
    ReturnType = returnType;
  }

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");

    ReturnType.Print(depth + 1);

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}parameterTypes:");

    foreach (var parameterType in ParameterTypes)
      parameterType.Print(depth + 2);
  }
}
