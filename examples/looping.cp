
func void main() {
  var int i;
  
  i = 0;
  while (i < 10) {
    println(i);
    i = i + 1;
  }

  i = 0;
  do {
    println(i);
    i = i + 1;
  } while (i < 10);

  i = 0;
  while (1) {
    if (i < 3) continue;
    println(i);
    if (i == 10) break;
    i = i + 1;
  }
}
