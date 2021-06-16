import java.util.Scanner

// 値の範囲 (L, R) を表すクラス
class Range(val L: Int, val R: Int)

/*
数列の 2 つの成分(範囲がそれぞれ a, b で与えられる)がこの順番で与えられたときに、転倒する確率を求める関数
計算量 : O(1)

a の値をx(横)軸, b の値をy(縦)軸に取り、
a.L <= x <= a.R, b.L <= y <= b.R によって作られる長方形領域 Rect と半平面 x > y の交差領域 I を考える
戻り値は (I の格子点の数) / (Rect の格子点の数) で与えられる

次の 6 通りに場合分けする

(1) a.R <= b.L
	I は空集合
(2) a.L > b.R
	I は Rect 全体
(3) a.L <= b.L && a.R <= b.R
	I は三角形 (a.R, b.L), (a.R, a.R), (b.L, b.L)
(4) a.L <= b.L && a.R > b.R
	I は台形 (a.R, b.L), (a.R, b.R), (b.R, b.R), (b.L, b.L)
(5) a.L > b.L && a.R <= b.R
	I は台形 (a.R, b.L), (a.R, a.R), (a.L, a.L), (a.L, b.L)
(6) a.L > b.L && a.R > b.R
	I は五角形 → 余事象 (三角形) を考える (余事象だと直線 x == y 上の点を含むことに注意)
*/
fun calcInversionProbability(a: Range, b: Range): Double {
	if (a.R <= b.L) return 0.0
	if (a.L > b.R) return 1.0

	val w = a.R - a.L + 1
	val h = b.R - b.L + 1

	val area = w * h

	val numPoints =
		if (a.L <= b.L)
			if (a.R <= b.R)
				(a.R - b.L) * (a.R - b.L + 1) / 2
			else
				(2 * a.R - b.L - b.R) * h / 2
		else
			if (a.R <= b.R)
				(a.L + a.R - 2 * b.L) * w / 2
			else
				area - (b.R - a.L + 1) * (b.R - a.L + 2) / 2

	return numPoints.toDouble() / area
}

fun main() {
	val input = Scanner(System.`in`)
	val N = input.nextInt()

	val bounds = Array(N) { Range(input.nextInt(), input.nextInt()) }

	var ans = 0.0

	for (i in 1 until N)
		for (j in 0 until i)
			ans += calcInversionProbability(bounds[j], bounds[i])

	println(ans)
}
