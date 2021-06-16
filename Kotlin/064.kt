import java.util.Scanner
import kotlin.math.abs

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()
	val Q = input.nextInt()

	val A = LongArray(N) { input.nextLong() }

	val D = LongArray(N - 1) { A[it + 1] - A[it] }

	// AtCoder の Kotlin バージョンでは sumOf は使用できないため fold で和を計算
	// (sumBy だと Long にできない)
//	var ans = D.sumOf { abs(it) }
	var ans = D.fold(0L) { acc, l -> acc + abs(l) }

	for (i in 1 .. Q) {
		val L = input.nextInt() - 2
		val R = input.nextInt() - 1
		val V = input.nextLong()

		if (L >= 0) {
			ans -= abs(D[L])
			D[L] += V
			ans += abs(D[L])
		}

		if (R < N - 1) {
			ans -= abs(D[R])
			D[R] -= V
			ans += abs(D[R])
		}

		println(ans)
	}
}
