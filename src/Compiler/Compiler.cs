
public class Compiler
{
  private Lexer lexer;
  public Diagnostics Diagnostics { get; }

  public Compiler(Lexer lexer)
  {
    this.lexer = lexer;
    Diagnostics = lexer.Diagnostics;
  }

  public void Compile()
  {
    // TODO: Implement compilation.
    while (lexer.CurrentToken.Kind != TokenKind.Eof)
    {
      Console.WriteLine(lexer.CurrentToken);
      lexer.NextToken();
      if (Diagnostics.IsFatal()) return;
    }
    Diagnostics.Report(MessageKind.Note, "Compilation finished successfully.");
  }
}
