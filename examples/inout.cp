
func void mutate(inout int x) {
  x = 4;
}

func void main() {
  var int[] a = [1, 2, 3];

  mutate(&a[0]);

  println(a);
}
