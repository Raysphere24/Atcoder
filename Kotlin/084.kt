fun main() {
	val n = readLine()!!.toInt()
	val s = readLine()!!

	val separators = (1 .. n).filter { it == n || s[it - 1] != s[it] }

	val ans = separators.zipWithNext { a, b -> a.toLong() * (b - a).toLong() } .sum()

	println(ans)
}
