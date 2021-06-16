import java.util.Scanner

fun A(n: Int): Int {
	var a = n
	var sum = n
	for (i in 1 .. 5) {
		sum += (a % 10)
		a /= 10
	}
	return sum % 100000
}

fun main(args: Array<String>) {
	val input = Scanner(System.`in`)
	var n = input.nextInt()
	val k = input.nextLong()

	val a = IntArray(100000) { -1 }

	var i = 0
	while (a[n] < 0) {
		a[n] = i
		n = A(n)
		i++
//		println("$i, $n")
		if (i.toLong() == k) {
			println(n)
			return
		}
	}

	val period = i - a[n]

	val l = (k - i) % period

	for (j in 1 .. l)
		n = A(n)

	println(n)
}