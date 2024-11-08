
func void main() {
  var int[] a;
  a = [];
  a = [1, 2, 3];
  a[0] = 4;

  println(a);
  println(a[0]);

  var float[][] b;
  b = [[]];
  b = [[1.0, 2.0], [3.0, 4.0]];
  b[0] = [5.0, 6.0];
  b[0][0] = 7.0;

  println(b);
  println(b[0]);
  println(b[0][0]);
}
