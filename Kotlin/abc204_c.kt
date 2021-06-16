import java.util.Scanner
 
class Vertex {
	val neighbors = ArrayList<Vertex>()
 
	fun reachableVertices(output: HashSet<Vertex>) {
		if (!output.add(this)) return
 
		for (n in neighbors)
			n.reachableVertices(output)
	}
}
 
fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()
	val M = input.nextInt()
 
	val vertices = Array(N) { Vertex() }
 
	for (i in 0 until M) {
		val a = input.nextInt() - 1
		val b = input.nextInt() - 1
		vertices[a].neighbors.add(vertices[b])
	}
 
	val set = HashSet<Vertex>()
 
	val ans = vertices.sumBy {
		set.clear()
		it.reachableVertices(set)
		set.count()
	}
 
	println(ans)
}
