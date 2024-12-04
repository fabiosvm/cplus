
public class FunctionTypeAnnotation : TypeAnnotation
{
  public override string Name => "function " + ReturnType.Name + "(" + string.Join(", ", ParameterTypes.Select(p => p.Name)) + ")";
  public TypeAnnotation ReturnType { get; }
  public List<ParamTypeAnnotation> ParameterTypes { get; } = new List<ParamTypeAnnotation>();

  public FunctionTypeAnnotation(TypeAnnotation returnType)
  {
    ReturnType = returnType;
  }

  public FunctionTypeAnnotation(FuncDeclNode funcDecl)
  {
    var retType = funcDecl.Children[0];
    ReturnType = TypeAnnotation.FromNode(retType);

    var paramList = (ParamListNode) funcDecl.Children[2];

    foreach (var param in paramList.Children)
    {
      var paramNode = (ParamNode) param;
      var annotation = new ParamTypeAnnotation(paramNode);
      ParameterTypes.Add(annotation);
    }
  }
}
