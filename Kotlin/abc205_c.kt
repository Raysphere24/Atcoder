import java.util.Scanner
import kotlin.math.*

fun main() {
	val input = Scanner(System.`in`)
	val A = input.nextInt()
	val B = input.nextInt()
	val C = input.nextInt()

	val diff = if (C % 2 == 0) abs(A) - abs(B) else A - B

	println(when (diff.sign) { 1 -> ">"; -1 -> "<" else -> "=" })
}
