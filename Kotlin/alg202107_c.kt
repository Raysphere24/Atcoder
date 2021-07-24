fun main(args: Array<String>) {
	val s = readLine()!!
	val (l, r) = readLine()!!.split(' ').map { it.toInt() }
	val ans = (s == "0" || !s.startsWith('0') && s.length <= 10) && s.toLong() in l .. r

	println(if (ans) "Yes" else "No")
}