import java.util.Scanner
import java.lang.Math.floorMod

fun main() {
	val input = Scanner(System.`in`)

	val N = input.nextInt()
	val Q = input.nextInt()
	val A = IntArray(N) { input.nextInt() }

	// どちらでも動きます
	fun swap(i: Int, j: Int) { A[i] = A[j].also { A[j] = A[i] } }
//	fun swap(i: Int, j: Int) { val temp = A[i]; A[i] = A[j]; A[j] = temp }

	var shift = 1
	for (i in 0 until Q) {
		val t = input.nextInt()
		val x = input.nextInt()
		val y = input.nextInt()

		when (t) {
			1 -> swap(floorMod(x - shift, N), floorMod(y - shift, N))
			2 -> shift++
			3 -> println(A[floorMod(x - shift, N)])
		}
	}
}
