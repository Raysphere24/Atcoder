import java.util.*
import kotlin.collections.ArrayList

class Connection(val target: Vertex, val distance: Int)

class Vertex(val index: Int)
{
	val neighbors = ArrayList<Connection>()
}

class SearchNode(private val priority: Int, private val vertex: Vertex)
	: Comparable<SearchNode>
{
	override fun compareTo(other: SearchNode) = if (priority < other.priority) -1 else 1

	fun process(queue: PriorityQueue<SearchNode>, distance: Array<Int>) {
		if (distance[vertex.index] >= 0) return

		distance[vertex.index] = priority

		for (connection in vertex.neighbors) {
			if (distance[connection.target.index] >= 0) continue

			val f = priority + connection.distance
			queue.add(SearchNode(f, connection.target))
		}
	}
}

fun findDistance(vertices: List<Vertex>, source: Vertex): Array<Int>
{
	val distance = Array<Int>(vertices.size) { -1 }

	val queue = PriorityQueue<SearchNode>()

	queue.add(SearchNode(0, source))

	while (!queue.isEmpty()) {
		queue.poll().process(queue, distance)
	}

	return distance
}

fun main()
{
	val input = Scanner(System.`in`)
	val n = input.nextInt()
	val m = input.nextInt()

	val vertices = (0 until n).map { Vertex(it) }

	for (i in 0 until m) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1
		val c = input.nextInt()

		vertices[a].neighbors.add(Connection(vertices[b], c))
		vertices[b].neighbors.add(Connection(vertices[a], c))
	}

	val distance_from_1 = findDistance(vertices, vertices[0])
	val distance_from_n = findDistance(vertices, vertices[n - 1])

	val answer = distance_from_1.zip(distance_from_n) { a, b -> a + b }

	print(answer.joinToString(separator = "\n"))
}
