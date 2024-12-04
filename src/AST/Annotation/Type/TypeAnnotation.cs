
using System.Diagnostics;

abstract public class TypeAnnotation : Annotation
{
  public static readonly TypeAnnotation Void = new VoidTypeAnnotation();
  public static readonly TypeAnnotation Bool = new BoolTypeAnnotation();

  public static TypeAnnotation FromNode(Node node)
  {
    if (node is VoidTypeNode) return Void;
    if (node is BoolTypeNode) return Bool;
    if (node is CharTypeNode) return IntegerTypeAnnotation.Char;
    if (node is ShortTypeNode) return IntegerTypeAnnotation.Short;
    if (node is IntTypeNode) return IntegerTypeAnnotation.Int;
    if (node is LongTypeNode) return IntegerTypeAnnotation.Long;

    if (node is UnsignedTypeNode unsignedTypeNode)
    {
      var intType = unsignedTypeNode.Children[0];

      if (intType is CharTypeNode) return UnsignedIntegerTypeAnnotation.UnsignedChar;
      if (intType is ShortTypeNode) return UnsignedIntegerTypeAnnotation.UnsignedShort;
      if (intType is IntTypeNode) return UnsignedIntegerTypeAnnotation.UnsignedInt;
      if (intType is LongTypeNode) return UnsignedIntegerTypeAnnotation.UnsignedLong;

      throw new UnreachableException();
    }

    if (node is FloatTypeNode) return FloatTypeAnnotation.Float;
    if (node is DoubleTypeNode) return FloatTypeAnnotation.Double;

    if (node is ArrayTypeNode arrayType)
      return new ArrayTypeAnnotation(arrayType);

    if (node is FuncDeclNode funcDecl)
      return new FunctionTypeAnnotation(funcDecl);

    throw new UnreachableException();
  }
}
