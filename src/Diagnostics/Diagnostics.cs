
public class Diagnostics
{
  public List<Message> Messages { get; } = new List<Message>();

  public void Note(string file, int line, int column, string text) => Report(file, line, column, MessageKind.Note, text);

  public void Warning(string file, int line, int column, string text) => Report(file, line, column, MessageKind.Warning, text);

  public void Error(string file, int line, int column, string text) => Report(file, line, column, MessageKind.Error, text);

  public void Fatal(string file, int line, int column, string text) => Report(file, line, column, MessageKind.Fatal, text);

  public void Report(string file, int line, int column, MessageKind kind, string text)
  {
    Messages.Add(new Message(file, line, column, kind, text));
  }

  public bool HasErrors()
  {
    return Messages.Any(message => message.Kind > MessageKind.Warning);
  }

  public bool IsFatal()
  {
    if (Messages.Count == 0)
      return false;
    var last = Messages.Count - 1;
    return Messages[last].Kind == MessageKind.Fatal;
  }

  public void Clear()
  {
    Messages.Clear();
  }

  public void Print()
  {
    foreach (var message in Messages)
      message.Print();
  }
}
