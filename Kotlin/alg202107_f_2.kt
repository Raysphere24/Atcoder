fun main(args: Array<String>) {
	val n = readLine()!!.toInt()

	val table = Array(100000) { BooleanArray(24) }

	repeat(n) {
		val (d, s, t) = readLine()!!.split(' ').map { it.toInt() }
		if (table[d - 1].slice(s until t).any { it }) {
			println("Yes")
			return
		}
		table[d - 1].fill(true, s, t)
	}

	println("No")
}