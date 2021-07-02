import java.util.Scanner

fun main() {
	with(Scanner(System.`in`)) {
		val count = IntArray(nextInt())

		repeat(nextInt()) {
			count[maxOf(nextInt(), nextInt()) - 1]++
		}

		println(count.count { it == 1 })
	}
}
