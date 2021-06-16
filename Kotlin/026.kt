import java.util.*

// id is 1-based
class Vertex(val id: Int) {
	val neighbors = ArrayList<Vertex>()
	var group = -1
}

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()

	val vertices = (1 .. N).map { Vertex(it) }

	for (i in 0 until N - 1) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1

		vertices[a].neighbors.add(vertices[b])
		vertices[b].neighbors.add(vertices[a])
	}

	val stack = Stack<Vertex>()

	vertices[0].group = 0
	stack.push(vertices[0])

	while (!stack.empty()) {
		val v = stack.pop()

		for (n in v.neighbors) {
			if (n.group >= 0) continue
			n.group = 1 - v.group

			stack.push(n)
		}
	}

	val numVerticesOfGroup0 = vertices.count { it.group == 0 }
	val chosenGroup = if (numVerticesOfGroup0 >= N / 2) 0 else 1

	vertices.filter { it.group == chosenGroup } .take(N / 2).forEach { println(it.id) }
}
