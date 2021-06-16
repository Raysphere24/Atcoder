import java.util.*

class SearchNode(parent: SearchNode?, val r: Int, val c: Int, val movement: Char)
	: Comparable<SearchNode> {
	val turns: Int =
		if (parent == null) -1
		else if (movement == parent.movement) parent.turns
		else parent.turns + 1

	override fun compareTo(other: SearchNode): Int = turns - other.turns
}

fun main() {
	val (R, C) = readLine()!!.split(' ').map { it.toInt() - 1 }
	val (rs, cs) = readLine()!!.split(' ').map { it.toInt() - 1 }
	val (rt, ct) = readLine()!!.split(' ').map { it.toInt() - 1 }

	val M = (0 .. R).map { readLine()!!.toCharArray() }

	val queue = PriorityQueue<SearchNode>()
	queue.add(SearchNode(null, rs, cs, '+'))

	while (!queue.isEmpty()) {
		with (queue.poll()) {
			if (r == rt && c == ct) {
				println(turns)
//				M[r][c] = '⁕';
//				M.forEach { println(it) }
				return
			}

			if (M[r][c] != '.') return@with

			M[r][c] = movement

			if (movement != '↓' && r > 0 && M[r - 1][c] == '.')
				queue.add(SearchNode(this, r - 1, c, '↑'))

			if (movement != '↑' && r < R && M[r + 1][c] == '.')
				queue.add(SearchNode(this, r + 1, c, '↓'))

			if (movement != '→' && c > 0 && M[r][c - 1] == '.')
				queue.add(SearchNode(this, r, c - 1, '←'))

			if (movement != '←' && c < C && M[r][c + 1] == '.')
				queue.add(SearchNode(this, r, c + 1, '→'))
		}
	}

	println("Failed")
}