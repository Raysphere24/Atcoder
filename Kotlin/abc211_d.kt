import java.util.*

class Vertex {
	val neighbors = ArrayList<Vertex>()
	var distance = Int.MAX_VALUE
	var numPaths = 0
}

fun main(args: Array<String>) {
	val input = Scanner(System.`in`)
	val vertices = Array(input.nextInt()) { Vertex() }

	repeat(input.nextInt()) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1
		vertices[a].neighbors.add(vertices[b])
		vertices[b].neighbors.add(vertices[a])
	}

	val start = vertices.first()
	val goal = vertices.last()

	val queue = ArrayDeque<Vertex>()
	start.distance = 0
	start.numPaths = 1
	queue.addFirst(start)

	while (queue.isNotEmpty()) {
		val vertex = queue.pollLast()

		if (vertex === goal) break

		for (neighbor in vertex.neighbors) {
			if (neighbor.distance <= vertex.distance) continue

			if (neighbor.numPaths == 0) {
				neighbor.distance = vertex.distance + 1
				queue.addFirst(neighbor)
			}

			neighbor.numPaths += vertex.numPaths
			neighbor.numPaths %= 1000000007
		}
	}

	println(goal.numPaths)
}
