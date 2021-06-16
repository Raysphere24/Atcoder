import java.util.Scanner

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()
	val K = input.nextInt()

	val C = LongArray(2 * N)
//	val C = ArrayList<Long>(2 * N)

	for (i in 0 until N) {
		val a = input.nextLong()
		val b = input.nextLong()

		C[2 * i] = b
		C[2 * i + 1] = a - b

//		C.add(b)
//		C.add(a - b)
	}

	C.sortDescending()

	println(C.take(K).sum())
}
