fun main() {
	val constraints = readLine()!!

	val answer = (0 .. 9999).count {
		val s = it.toString().padStart(4, '0')
		constraints.withIndex().all {
			when (it.value) {
				'o' -> (0 .. 3).any { i -> s[i] - '0' == it.index }
				'x' -> (0 .. 3).all { i -> s[i] - '0' != it.index }
				else -> true
			}
		}
	}

	println(answer)
}