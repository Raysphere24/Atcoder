import java.util.Scanner
 
fun main() {
	val input = Scanner(System.`in`)
 
	val N = input.nextInt()
	var K = input.nextLong()
 
	val f = HashMap<Long, Long>()
 
	for (i in 1 .. N) {
		val a = input.nextLong()
		val b = input.nextLong()
 
		f[a] = (f[a] ?: 0) + b
	}
 
	val g = f.toSortedMap()
 
	var village = 0L
 
	for ((a, b) in g) {
		if (K < a - village) break
 
		K += b - (a - village)
		village = a
	}
 
	println(village + K)
}
