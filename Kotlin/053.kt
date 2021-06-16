import kotlin.math.*

const val sqrt5 = 2.23606797749979

// the golden ratio
const val phi = (sqrt5 + 1) / 2

// 1 -> 1, 2 -> 1, 3 -> 2, 4 -> 3, 5 -> 5, 6 -> 8, 7 -> 13 ...
fun fibonacci(index: Int) = round(phi.pow(index) / sqrt5).toInt()

// 1      -> 3
// 2      -> 4
// 3 .. 4 -> 5
// 5 .. 7 -> 6
// 8 ..12 -> 7
fun nextFibonacciIndex(number: Int) = ceil(log(number * sqrt5 + 0.5, phi)).toInt()

// クエリを行う
fun getValue(index: Int): Int {
	if (index <= 0) return Int.MIN_VALUE // 範囲外クエリ
	println("? $index")
	return readLine()!!.toInt()
}

// インデックスと値の組を表すクラス
class Sample(val index: Int) {
	val value = getValue(index)
	override fun toString() = "($index, $value)"
}

// a, b: 探索区間の両端 (探索区間は両端を含まない)
// x, y: フィボナッチ分割位置におけるインデックスと値の組
fun search(a: Int, b: Int, x: Sample, y: Sample): Int {
	if (b - a == 3)
		return max(x.value, y.value)

	val offset = y.index - x.index

	if (x.value <= y.value)
		return search(x.index, b, y, Sample(b - offset))
	else
		return search(a, y.index, Sample(a + offset), x)
}

fun main() {
	val numSequences = readLine()!!.toInt()

	for (iteration in 1 .. numSequences) {
		val length = readLine()!!.toInt()

		val result = if (length == 1) getValue(1) else {
			// n は n > length となる最小のフィボナッチ数
			val i = nextFibonacciIndex(length)
			val n = fibonacci(i)

			// getValue 関数に length を渡さなくてもいいように負側に拡張する
			val a = length + 1 - n
			val b = length + 1

			val x = Sample(a + fibonacci(i - 2))
			val y = Sample(a + fibonacci(i - 1))

			search(a, b, x, y)
		}

		println("! $result")
	}
}
