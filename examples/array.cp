
func void main() {
  var int[] a;
  a = {};
  a = {1, 2, 3};
  a[0] = 4;
  println(a);
  println(a[0]);

  var int[][] b;
  b = {{}};
  b = {{1, 2}, {3, 4}};
  b[0] = {5, 6};
  b[0][0] = 7;
  println(b);
  println(b[0]);
  println(b[0][0]);

  var int[] c = new int[] {1, 2, 3};
  println(c);

  var int[] d = {};
  append(&d, 1);
  append(&d, 2);
  append(&d, 3);
  println(d);

  var int[][] e = {{1, 2}, {3, 4}};
  println(e);
}
