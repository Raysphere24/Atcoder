import java.util.*
import kotlin.collections.ArrayList

class Connection(val target: Int, val distance: Int)

class SearchNode(private val priority: Int, private val vertex: Int) : Comparable<SearchNode>
{
	override fun compareTo(other: SearchNode) = if (priority < other.priority) -1 else 1

	fun process(connections: List<ArrayList<Connection>>, queue: PriorityQueue<SearchNode>, distance: IntArray) {
		if (distance[vertex] >= 0) return

		distance[vertex] = priority

		for (connection in connections[vertex]) {
			if (distance[connection.target] >= 0) continue

			val f = priority + connection.distance
			queue.add(SearchNode(f, connection.target))
		}
	}
}

fun findDistance(connections: List<ArrayList<Connection>>, source: Int): IntArray
{
	val distance = IntArray(connections.size) { -1 }

	val queue = PriorityQueue<SearchNode>()

	queue.add(SearchNode(0, source))

	while (!queue.isEmpty()) {
		queue.poll().process(connections, queue, distance)
	}

	return distance
}

fun main()
{
	val input = Scanner(System.`in`)
	val n = input.nextInt()
	val m = input.nextInt()

	val connections = (1 .. n).map { ArrayList<Connection>(4) }

	for (i in 1 .. m) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1
		val c = input.nextInt()

		connections[a].add(Connection(b, c))
		connections[b].add(Connection(a, c))
	}

	val distanceFrom1 = findDistance(connections, 0)
	val distanceFromN = findDistance(connections, n - 1)

	val answer = distanceFrom1.zip(distanceFromN) { a, b -> a + b }

	print(answer.joinToString(separator = "\n"))
}
