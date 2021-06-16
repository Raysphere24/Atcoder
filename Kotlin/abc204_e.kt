import java.util.*
import kotlin.collections.ArrayList
import kotlin.math.*

class Connection(val target: Vertex, val c: Long, val d: Long) {
	val minTime = round(sqrt(d.toDouble()) - 1).toLong()

	fun calcTime(time: Long) = time + c + d / (time + 1)
}

class Vertex {
	val connections = ArrayList<Connection>()
	var distance = -1L
}

class SearchNode(val target: Vertex, val time: Long) : Comparable<SearchNode> {
	override fun compareTo(other: SearchNode) = (time - other.time).sign
}

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()
	val M = input.nextInt()

	val vertices = Array(N) { Vertex() }

	for (i in 0 until M) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1
		val c = input.nextLong()
		val d = input.nextLong()
		vertices[a].connections.add(Connection(vertices[b], c, d))
		vertices[b].connections.add(Connection(vertices[a], c, d))
	}

	val goal = vertices.last()

	val queue = PriorityQueue<SearchNode>()
	queue.add(SearchNode(vertices[0], 0))

	while (queue.isNotEmpty()) {
		val node = queue.poll()

		if (node.target.distance >= 0) continue
		node.target.distance = node.time

		if (node.target == goal) break

		for (connection in node.target.connections) {
			if (connection.target.distance >= 0) continue
			val t = max(connection.minTime, node.time)
			queue.add(SearchNode(connection.target, connection.calcTime(t)))
		}
	}

	println(goal.distance)
}
