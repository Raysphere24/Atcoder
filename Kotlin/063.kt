import java.util.Scanner
import kotlin.math.max

fun main() {
	val input = Scanner(System.`in`)
	val H = input.nextInt()
	val W = input.nextInt()

	val P = Array(H) { IntArray(W) { input.nextInt() } }

	val stack = Array(H) { IntArray(W) }

	val count = IntArray(H * W)

	fun search(currentRowIndex: Int, assignedRows: Int): Int {
		if (currentRowIndex == H) {
			if (assignedRows == 0) return 0

			count.fill(0)
			for (x in stack[assignedRows - 1]) {
				if (x > 0) count[x - 1]++
			}

			return count.max()!! * assignedRows
		}

		val a = search(currentRowIndex + 1, assignedRows)

		if (assignedRows == 0) {
			P[currentRowIndex].copyInto(stack[0])
		}
		else {
			val prevStackRow = stack[assignedRows - 1]
			val currentStackRow = stack[assignedRows]

			for ((i, x) in P[currentRowIndex].withIndex()) {
				currentStackRow[i] = if (prevStackRow[i] == x) x else 0
			}
		}

		val b = search(currentRowIndex + 1, assignedRows + 1)

		return max(a, b)
	}

	println(search(0, 0))
}
