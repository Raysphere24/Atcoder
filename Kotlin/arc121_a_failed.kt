import java.util.Scanner
import kotlin.math.abs
import kotlin.math.max

class Point(val x: Int, val y: Int)

fun distance(p: Point, q: Point) = max(abs(p.x - q.x), abs(p.y - q.y))

inline fun <T, R : Comparable<R>> Array<T>.maxOf(selector: (T) -> R) = selector(maxBy(selector)!!)

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()

	val P = Array(N) { Point(input.nextInt(), input.nextInt()) }

//	if (N == 3) {
//		val distances = arrayOf(
//			distance(P[0], P[1]),
//			distance(P[1], P[2]),
//			distance(P[2], P[0])
//		)
//		distances.sort()
//		println(distances[1])
//		return
//	}

	val bounds = arrayOf(
		P.minBy { it.x }!!,
		P.maxBy { it.x }!!,
		P.minBy { it.y }!!,
		P.maxBy { it.y }!!
	)

	val a = distance(bounds[0], bounds[1])
	val b = distance(bounds[2], bounds[3])

	if (a > b) {
		val c = P.maxOf { p -> if (p != bounds[0] && p != bounds[1]) bounds.maxOf { distance(p, it) } else 0 }
		println(max(b, c))
	}
	else {
		val c = P.maxOf { p -> if (p != bounds[2] && p != bounds[3]) bounds.maxOf { distance(p, it) } else 0 }
		println(max(a, c))
	}
}
