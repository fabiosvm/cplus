
public class TypeAnnotation : Annotation
{
  public static readonly TypeAnnotation Void = new TypeAnnotation(TypeAnnotationKind.Void);
  public static readonly TypeAnnotation Char = new TypeAnnotation(TypeAnnotationKind.Char);
  public static readonly TypeAnnotation Short = new TypeAnnotation(TypeAnnotationKind.Short);
  public static readonly TypeAnnotation Int = new TypeAnnotation(TypeAnnotationKind.Int);
  public static readonly TypeAnnotation Long = new TypeAnnotation(TypeAnnotationKind.Long);
  public static readonly TypeAnnotation UnsignedChar = new TypeAnnotation(TypeAnnotationKind.UnsignedChar);
  public static readonly TypeAnnotation UnsignedShort = new TypeAnnotation(TypeAnnotationKind.UnsignedShort);
  public static readonly TypeAnnotation UnsignedInt = new TypeAnnotation(TypeAnnotationKind.UnsignedInt);
  public static readonly TypeAnnotation UnsignedLong = new TypeAnnotation(TypeAnnotationKind.UnsignedLong);
  public static readonly TypeAnnotation Float = new TypeAnnotation(TypeAnnotationKind.Float);
  public static readonly TypeAnnotation Double = new TypeAnnotation(TypeAnnotationKind.Double);

  public TypeAnnotationKind Kind { get; }

  public TypeAnnotation(TypeAnnotationKind kind)
  {
    Kind = kind;
  }

  public override int GetHashCode()
  {
    return Kind.GetHashCode();
  }

  public override bool Equals(object? obj)
  {
    if (obj == null || GetType() != obj.GetType())
      return false;
    return Kind == ((TypeAnnotation) obj).Kind;
  }
}
