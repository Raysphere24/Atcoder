fun main() {
	val (N, L) = readLine()!!.split(' ').map(String::toInt)

	val m = 1000000007

	val A = IntArray(N + 1)
	A[0] = 1

	for (i in 1 .. N) {
		A[i] = A[i - 1]
		if (i >= L) {
			A[i] += A[i - L]
			A[i] %= m
		}
	}

	println(A[N])
}
