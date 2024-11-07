internal static class Program
{
  private static int Main()
  {
    var source = "function int main() {\n  return 0;\n}";

    var compiler = new Compiler(source);
    compiler.Compile();

    compiler.Diagnostics.Print();

    return 0;
  }
}
