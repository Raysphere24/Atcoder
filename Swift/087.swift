import Foundation

func readInt3() -> (Int, Int, Int) {
	let s = readLine()!.split(separator: " ")
	return (Int(s[0])!, Int(s[1])!, Int(s[2])!)
}

extension Comparable {
	mutating func minAssign(_ other: Self) { self = min(self, other) }
}

let (N, P, K) = readInt3()

// 入力の距離データは flatMap で1次元配列に潰して読む
let A: [Int] = (0 ..< N).flatMap { r in readLine()!.split(separator: " ").map { Int($0)! } }

// 全点対最短距離計算
func calcAllPairDistance(_ X: Int) -> [Int] {
	// 入力の距離データの -1 を X に置き換えて dist とする
	var dist: [Int] = A.map { $0 >= 0 ? $0 : X }

	// Floyd–Warshall algorithm
	for k in 0 ..< N {
		for i in 0 ..< N {
			for j in 0 ..< N {
				dist[i * N + j].minAssign(dist[i * N + k] + dist[k * N + j])
			}
		}
	}

	return dist
}

// 距離が P 以下の点対を数える
func countPairs(_ dist: [Int]) -> Int {
	var ret = 0
	for j in 0 ..< N {
		for i in 0 ..< j {
			if dist[i * N + j] <= P {
				ret += 1
			}
		}
	}
	return ret
}

// X = 1 および X が十分大きい (4000000000 > N * A_{ij}) ときの全点対距離
let min_dist = calcAllPairDistance(1)
let max_dist = calcAllPairDistance(4000000000)

// K が取りうる最大/最小 (max/min が反転している)
let max_possible_K = countPairs(min_dist)
let min_possible_K = countPairs(max_dist)

if (K < min_possible_K || K > max_possible_K) {
	print(0)
}
else if (K == min_possible_K) {
	print("Infinity")
}
else {
	let initial_hi = max_dist.max()!

	// 2分探索で countPairs(calcAllPairDistance(X)) == value となる最小の X を探す
	func binarySearch(value: Int) -> Int {
		var lo = 0
		var hi = initial_hi

		while (hi - lo > 1) {
			let mid = (lo + hi) / 2

			if countPairs(calcAllPairDistance(mid)) <= value {
				hi = mid
			} else {
				lo = mid
			}
		}

		return hi
	}

	let lower = binarySearch(value: K)
	let upper = binarySearch(value: K - 1)
	
	print(upper - lower)
}
