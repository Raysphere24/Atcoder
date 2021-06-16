fun calc(S: List<String>, i: Int): Long =
	if (i == 0)
		1
	else if (S[i - 1] == "AND")
		calc(S, i - 1)
	else
		calc(S, i - 1) + (1L shl i)

fun main()
{
	val N = readLine()!!.toInt()
	val S = (1 .. N).map { readLine()!! }

	println(calc(S, N))
}
