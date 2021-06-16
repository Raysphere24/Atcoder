import java.util.Scanner

class Point(val x: Int, val y: Int) : Comparable<Point> {
	override fun compareTo(other: Point): Int {
		if (x != other.x) return x - other.x
		return y - other.y
	}

	override fun toString() = "($x, $y)"
}

fun main() {
	val input = Scanner(System.`in`)

	val N = input.nextInt()
	val M = input.nextInt()

	val pawns = Array(M) { Point(input.nextInt(), input.nextInt()) }

	pawns.sort()

//	println(pawns.joinToString())

	val set = HashSet<Int>()

	set.add(N)

	var prev = Point(0, N)
	var prev_contains = false

//	println(set.joinToString())

	for (p in pawns) {
		set.remove(p.y)

		if (set.contains(p.y - 1) || prev.x == p.x && prev.y == p.y - 1 && prev_contains)
			set.add(p.y)

		if (set.contains(p.y + 1))
			set.add(p.y)

		prev = p
		prev_contains = set.contains(p.y)
//		println(set.joinToString())
	}

	println(set.size)
}
