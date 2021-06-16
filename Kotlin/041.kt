import java.util.*
import kotlin.Comparator
import kotlin.collections.ArrayList
import kotlin.math.abs
import kotlin.math.sign

class Point(val x: Long, val y: Long) : Comparable<Point> {
	override fun compareTo(other: Point): Int {
		if (x != other.x) return (x - other.x).sign
		return (y - other.y).sign
	}
	override fun toString(): String = "($x, $y)"
}

/*
	|p.x a.x b.x|   |a.x - p.x b.x - p.x|
	|p.y a.y b.y| = |a.y - p.y b.y - p.y|
	| 1   1   1 |

	符号: a→p→b が時計回りのとき > 0, 反時計回りのとき < 0, 一直線上のとき == 0
	絶対値: 三角形 pab の面積の 2 倍
 */
fun det(p: Point, a: Point, b: Point): Long = (a.x - p.x) * (b.y - p.y) - (b.x - p.x) * (a.y - p.y)

fun gcd(a: Long, b: Long): Long = if (b == 0L) a else gcd(b, a % b)

fun ArrayList<Point>.removeLast() = removeAt(size - 1)

fun internalAppendHullChain(points: List<Point>, p: ArrayList<Point>) {
	for (point in points) {
		while (p.size > 1 && det(p[p.size - 1], p[p.size - 2], point) > 0)
			p.removeLast()

		p.add(point)
	}
}

// Monotone Chain アルゴリズムによる凸法の計算
fun calcConvexHullUsingMonotoneChain(points: List<Point>): List<Point> {
	val lexicographicallySortedPoints = points.sorted()
	val result = ArrayList<Point>()

	internalAppendHullChain(lexicographicallySortedPoints, result)
	result.removeLast()

	internalAppendHullChain(lexicographicallySortedPoints.reversed(), result)
	result.removeLast()

	return result
}

// Graham Scan アルゴリズムによる凸法の計算
fun calcConvexHullUsingGrahamScan(points: List<Point>): ArrayList<Point> {
	val lexicographicallySortedPoints = points.sorted()
	val basePoint = lexicographicallySortedPoints[0]
	val sortedPoints = lexicographicallySortedPoints.drop(1)
		.sortedWith(Comparator { a, b -> det(basePoint, b, a).sign } )

	var prevPoint = sortedPoints[0]
	val sortedPointsWithoutCollinear = ArrayList<Point>()

	for (point in sortedPoints.drop(1)) {
		if (det(basePoint, prevPoint, point) != 0L)
			sortedPointsWithoutCollinear.add(prevPoint)
		prevPoint = point
	}
	sortedPointsWithoutCollinear.add(prevPoint)

	val result = arrayListOf(basePoint)

	internalAppendHullChain(sortedPointsWithoutCollinear, result)

	return result
}

// 多角形の面積の 2 倍
fun calcTwiceOfArea(polygon: List<Point>): Long {
	val p = polygon[0]
	var a = polygon[1]
	var result = 0L

	for (b in polygon.drop(2)) {
		result += det(p, a, b)
		a = b
	}

	return result
}

// 多角形の周上の格子点の数
fun calcNumLatticePointsOnBoundary(polygon: List<Point>): Long {
	var a = polygon.last()
	var result = 0L

	for (b in polygon) {
		result += gcd(abs(a.x - b.x), abs(a.y - b.y))
		a = b
	}

	return result
}

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()

	val points = (1 .. N).map { Point(input.nextLong(), input.nextLong()) }

	// 凸法の計算はどちらでもできます
//	val convexHull = calcConvexHullUsingGrahamScan(points)
	val convexHull = calcConvexHullUsingMonotoneChain(points)
//	println(convexHull)

	val twiceOfArea = calcTwiceOfArea(convexHull)
	val numLatticePointsOnBoundary = calcNumLatticePointsOnBoundary(convexHull)

	// ピックの定理
	val numLatticePointsInsidePolygon = (twiceOfArea - numLatticePointsOnBoundary) / 2 + 1

	val answer = numLatticePointsOnBoundary + numLatticePointsInsidePolygon - N

	println(answer)
}