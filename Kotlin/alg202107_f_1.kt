//import java.util.Scanner

fun main(args: Array<String>) {
//	val input = Scanner(System.`in`)
//	val n = input.nextInt()
	val n = readLine()!!.toInt()

//	val a = Array(n) { Triple(input.nextInt(), input.nextInt(), input.nextInt()) }
	val a = Array(n) { readLine()!!.split(' ').map { it.toInt() } }

//	val b: Map<Int, List<Triple<Int, Int, Int>>> = a.groupBy { it.first }
	val b: Map<Int, List<List<Int>>> = a.groupBy { it[0] }

	val table = BooleanArray(24)

	for (r in b.values) {
		table.fill(false)
		for ((d, s, t) in r) {
			if (table.slice(s until t).any { it }) {
				println("Yes")
				return
			}
			table.fill(true, s, t)
		}
	}

	println("No")
}