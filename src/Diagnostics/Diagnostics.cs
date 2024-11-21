
public class Diagnostics
{
  public List<Message> Messages { get; } = new List<Message>();

  public void Note(string text) => Report(MessageKind.Note, text);

  public void Warning(string text) => Report(MessageKind.Warning, text);

  public void Error(string text) => Report(MessageKind.Error, text);

  public void Fatal(string text) => Report(MessageKind.Fatal, text);

  public void Report(MessageKind kind, string text)
  {
    Messages.Add(new Message(kind, text));
  }

  public bool HasErrors()
  {
    return Messages.Any(message => message.Kind == MessageKind.Error);
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
      Console.WriteLine(message);
  }
}
