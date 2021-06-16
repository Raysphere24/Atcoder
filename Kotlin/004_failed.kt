import java.util.Scanner

fun main()
{
	val input = Scanner(System.`in`)
	val H = input.nextInt()
	val W = input.nextInt()

	val A = Array(H) { IntArray(W) { input.nextInt() } }

	val C = A.map { it.sum() }
	val R = IntArray(W) { x -> A.sumBy { it[x] } }

	for (i in 0 until H) {
		for (j in 0 until W) {
			print(C[i] + R[j] - A[i][j])
			print(' ')
		}
		println()
	}
}
