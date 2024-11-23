
abstract public class TypeAnnotation : Annotation
{
  public static readonly TypeAnnotation Void = new VoidTypeAnnotation();
  public static readonly TypeAnnotation Bool = new BoolTypeAnnotation();

  public bool HasInoutModifier { get; set; }
}
