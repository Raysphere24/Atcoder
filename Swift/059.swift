import Foundation

func readInt2Minus1() -> (Int, Int) {
	let s = readLine()!.split(separator: " ")
	return (Int(s[0])! - 1, Int(s[1])! - 1)
}

func readInt3() -> (Int, Int, Int) {
	let s = readLine()!.split(separator: " ")
	return (Int(s[0])!, Int(s[1])!, Int(s[2])!)
}

let (N, M, Q) = readInt3()

var edges = (0 ..< M).map { i in readInt2Minus1() }

edges.sort(by: <)

typealias SimdType = SIMD8<UInt64>

var bits = Array(repeating: SimdType(), count: N)
var goals: [Int] = []

var remainingQueries = Q

while remainingQueries > 0 {
	for i in 0 ..< N {
		bits[i] = SimdType()
	}
	goals.removeAll()

	outerLoop: for i in 0 ..< SimdType.scalarCount {
		var bit: UInt64 = 1
		while bit > 0 {
			let (a, b) = readInt2Minus1()
			bits[a][i] |= bit
			goals.append(b)

			bit <<= 1
			remainingQueries -= 1

			if (remainingQueries <= 0) {
				break outerLoop
			}
		}
	}

	for (a, b) in edges {
		bits[b] |= bits[a]
	}

	var index = 0
	outerLoop: for i in 0 ..< SimdType.scalarCount {
		var bit: UInt64 = 1
		bit = 1
		while bit > 0 {
			let goal = goals[index]
			let ans = (bits[goal][i] & bit) != 0

			print(ans ? "Yes" : "No")

			bit <<= 1

			index += 1
			if (index >= goals.count) {
				break outerLoop
			}
		}
	}
}
