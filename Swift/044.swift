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

var shift = N - 1

for _ in 0 ..< Q {
	let (t, x, y) = readInt3()
	
	switch t {
	case 1:
		A.swapAt((x + shift) % N, (y + shift) % N)
	case 2:
		shift = (N + shift - 1) % N
	default:
		print(A[(x + shift) % N])
	}
}
