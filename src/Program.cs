internal static class Program
{
  private static int Main()
  {
    var source = "function int main() {\n  return 0;\n}";
    var diagnostics = new Diagnostics();
    var lexer = new Lexer(source, diagnostics);
    var compiler = new Compiler(lexer);
    compiler.Compile();
    diagnostics.Print();
    return 0;
  }
}
