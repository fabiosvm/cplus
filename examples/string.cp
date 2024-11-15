
func void main() {
  var char[] s1 = "Hello  ";
  var char[] s2 = "world!";

  var char[] s3 = s1 + s2;
  s3[5] = ',';

  println(s3);
}
