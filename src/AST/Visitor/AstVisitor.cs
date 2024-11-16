
public class AstVisitor
{
  private Dictionary<Type, Action<Node>> enterActions = new Dictionary<Type, Action<Node>>();
  private Dictionary<Type, Action<Node>> exitActions = new Dictionary<Type, Action<Node>>();

  public void RegisterEnterAction<T>(Action<T> action) where T : Node
  {
    enterActions[typeof(T)] = node => action((T) node);
  }

  public void RegisterExitAction<T>(Action<T> action) where T : Node
  {
    exitActions[typeof(T)] = node => action((T) node);
  }

  public void Visit(Node node)
  {
    visitNode(node);
  }

  private void visitNode(Node node)
  {
    var type = node.GetType();

    if (enterActions.ContainsKey(type))
    {
      var action = enterActions[type];
      action(node);
    }

    if (node is NonLeafNode)
    {
      var nonLeafNode = (NonLeafNode) node;
      foreach (var child in nonLeafNode.Children)
        visitNode(child);
    }

    if (exitActions.ContainsKey(type))
    {
      var action = exitActions[type];
      action(node);
    }
  }
}
