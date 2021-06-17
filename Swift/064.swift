func readInt2() -> (Int, Int) {
	let s = readLine()!.split(separator: " ")
	return (Int(s[0])!, Int(s[1])!)
}

func readInt3() -> (Int, Int, Int) {
	let s = readLine()!.split(separator: " ")
	return (Int(s[0])!, Int(s[1])!, Int(s[2])!)
}

let (N, Q) = readInt2()

var A = readLine()!.split(separator: " ").map { Int($0)! }

var D = (1 ..< N).map { i in A[i] - A[i - 1] }

var ans = D.reduce(0) { acc, d in acc + abs(d) }

for _ in 0 ..< Q {
	let (L, R, V) = readInt3()
	let l = L - 2
	let r = R - 1
	
	if (l >= 0) {
		ans -= abs(D[l])
		D[l] += V
		ans += abs(D[l])
	}
	
	if (R < N) {
		ans -= abs(D[r])
		D[r] -= V
		ans += abs(D[r])
	}
	
	print(ans)
}
