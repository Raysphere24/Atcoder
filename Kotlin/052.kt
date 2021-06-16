fun main() {
	val N = readLine()!!.toInt()

	val m = 1000000007L

	val A = (0 until N).map { readLine()!!.split(' ').map(String::toLong) }

	val answer = A.fold(1L) { acc, list ->
		(acc * list.sum()) % m
	}

	println(answer)
}
