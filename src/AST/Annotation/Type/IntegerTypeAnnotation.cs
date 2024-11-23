
abstract public class IntegerTypeAnnotation : NumericTypeAnnotation
{
  public static readonly TypeAnnotation Char = new CharTypeAnnotation();
  public static readonly TypeAnnotation Short = new ShortTypeAnnotation();
  public static readonly TypeAnnotation Int = new IntTypeAnnotation();
  public static readonly TypeAnnotation Long = new LongTypeAnnotation();
}
