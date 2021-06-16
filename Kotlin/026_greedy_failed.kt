import java.util.Scanner
import kotlin.collections.ArrayList
import kotlin.collections.HashSet

class Vertex(private val label: Int)
{
	val neighbors = ArrayList<Vertex>()

	override fun hashCode() = label
	override fun toString() = label.toString()
}

fun main()
{
	val input = Scanner(System.`in`)
	val N = input.nextInt()

	val vertices = Array(N) { Vertex(it + 1) }

	for (i in 1 until N) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1

		vertices[a].neighbors.add(vertices[b])
		vertices[b].neighbors.add(vertices[a])
	}

	vertices.sortBy { it.neighbors.size }

	val hashset = HashSet<Vertex>()
	val M = N / 2

	for (vertex in vertices) {
		if (vertex.neighbors.any { hashset.contains(it) }) continue

		hashset.add(vertex)

		if (hashset.size == M) break
	}

	if (hashset.size != M) throw AssertionError()

	println(hashset.joinToString(separator = " "))
}
