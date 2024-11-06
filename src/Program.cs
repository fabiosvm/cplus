internal static class Program
{
  private static int Main()
  {
    var source = "int main() { return 0; }";
    var diagnostics = new Diagnostics();
    var lexer = new Lexer(source, diagnostics);
    var compiler = new Compiler(lexer);
    compiler.Compile();
    diagnostics.Print();
    return 0;
  }
}
