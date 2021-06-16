import java.util.*

class SearchState(val N: Int, val A: Array<IntArray>, val B: Array<BooleanArray>)
{
	private val isAssigned = BooleanArray(N)

	fun search(n_assigned: Int, current_cost: Int, previous: Int): Int
	{
		if (n_assigned == N) {
			return current_cost
		}

		var min_cost = Int.MAX_VALUE

		for (i in 0 until N) {
			if (isAssigned[i]) continue
			if (previous >= 0 && B[previous][i]) continue

			isAssigned[i] = true

			val new_cost = current_cost + A[i][n_assigned]

			val cost = search(n_assigned + 1, new_cost, i)

			isAssigned[i] = false

			if (min_cost > cost)
				min_cost = cost
		}

		return min_cost
	}
}

fun main()
{
	val input = Scanner(System.`in`)
	val N = input.nextInt()

	val A = Array(N) { IntArray(N) { input.nextInt() } }

	val M = input.nextInt()

	val B = Array(N) { BooleanArray(N) }

	for (i in 1 .. M) {
		val X = input.nextInt() - 1
		val Y = input.nextInt() - 1

		B[X][Y] = true
		B[Y][X] = true
	}

	val state = SearchState(N, A, B)

	val result = state.search(0, 0, -1)

	print(if (result == Int.MAX_VALUE) -1 else result)
}
