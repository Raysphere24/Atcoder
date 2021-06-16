import java.util.Scanner
import kotlin.math.abs

class Point(val x: Long, val y: Long)

// Kotlin 1.4 以降が AtCoder で使用できないため
fun <T, R : Comparable<R>> Array<T>.maxOf(selector: (T) -> R) = selector(maxBy(selector)!!)

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()
	val Q = input.nextInt()

	val points = Array(N) { Point(input.nextLong(), input.nextLong()) }

	val bounds = arrayOf(
		points.maxBy {  it.x + it.y }!!,
		points.maxBy { -it.x + it.y }!!,
		points.maxBy { -it.x - it.y }!!,
		points.maxBy {  it.x - it.y }!!
	)

	for (i in 1 .. Q) {
		val p = points[input.nextInt() - 1]
		val d = bounds.maxOf { abs(it.x - p.x) + abs(it.y - p.y) }

		println(d)
	}
}
