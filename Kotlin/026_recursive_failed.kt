import java.lang.Exception
import java.util.Scanner
import kotlin.collections.ArrayList
import kotlin.collections.HashSet

class Vertex(private val label: Int)
{
	val neighbors = ArrayList<Vertex>()

	override fun hashCode() = label
	override fun toString() = label.toString()
}

class SearchState(val vertices: Array<Vertex>)
{
	val hashset = HashSet<Vertex>()
	val M = vertices.size / 2

	private fun recursiveSearch(position: Int): Boolean
	{
		if (hashset.size == M)
			return true

		if (position == vertices.size)
			return false

		val vertex = vertices[position]

		if (!vertex.neighbors.any { hashset.contains(it) }) {
			hashset.add(vertex)

			if (recursiveSearch(position + 1))
				return true

			hashset.remove(vertex)
		}

		if (recursiveSearch(position + 1))
			return true

		return false
	}

	fun search()
	{
		if (!recursiveSearch(0)) throw Exception()

		println(hashset.joinToString(separator = " "))
	}
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

	val state = SearchState(vertices)

	state.search()
}
