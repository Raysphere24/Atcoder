import java.util.Scanner

fun main() {
	val input = Scanner(System.`in`)

	val count = IntArray(input.nextInt())

	repeat(input.nextInt()) {
		count[maxOf(input.nextInt(), input.nextInt()) - 1]++
	}

	println(count.count { it == 1 })
}
