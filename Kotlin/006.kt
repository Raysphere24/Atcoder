import java.lang.StringBuilder

fun main()
{
	val (N, K) = readLine()!!.split(' ').map(String::toInt)
	val S = readLine()!!

	val table = ('a' .. 'z').map { c ->
		val row = IntArray(S.length)
		var value = -1
		for (i in S.indices.reversed()) {
			if (S[i] == c) value = i
			row[i] = value
		}
		row
	}

	val answer = StringBuilder(K)
	var position = 0
	while (answer.length < K) {
		val lastFeasibleIndex = N - K + answer.length
		position = table.find { it[position] in 0 .. lastFeasibleIndex } !![position]
		answer.append(S[position])
		position++
	}

	println(answer)
}
