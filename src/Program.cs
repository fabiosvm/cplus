internal static class Program
{
  private static int Main()
  {
    var source = "function float sum(float a, float b) { return a + b; }";

    var compiler = new Compiler(source);
    compiler.Compile();

    compiler.Diagnostics.Print();

    return 0;
  }
}
