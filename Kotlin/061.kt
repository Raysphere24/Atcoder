import java.util.ArrayList
import java.util.Scanner

fun main() {
	val input = Scanner(System.`in`)

	val A = ArrayList<Int>()
	val B = ArrayList<Int>()

	repeat(input.nextInt()) {
		val t = input.nextInt()
		val x = input.nextInt()

		when (t) {
			1 -> A.add(x)
			2 -> B.add(x)
			3 -> println(if (x <= A.size) A[A.size - x] else B[x - 1 - A.size])
		}
	}
}
