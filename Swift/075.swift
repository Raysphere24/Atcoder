import Foundation

let N = UInt(readLine()!)!

// sqrt(N) 以下の奇素数のリスト
var oddPrimes: [UInt] = []

// 3 以上 √N 以下の奇数でループ
outerLoop: for i in stride(from: 3, through: UInt(sqrt(Double(N))), by: 2) {
	for p in oddPrimes {
		if i % p == 0 { continue outerLoop } // i は素数でない
		if p * p > i { break } // i は素数
	}
	oddPrimes.append(i)
}

// N の素因数の数を求める (sqrt(N) 以下の素数で順に割っていく)
func numPrimeFactors(_ N: UInt) -> Int {
	// 素数 2 を事前に処理しておく
	var result = N.trailingZeroBitCount
	var n = N >> result
	if (n == 1) { return result }

	// 奇素数 p で割っていく
	for p in oddPrimes {
		while (n % p == 0) {
			result += 1
			n /= p
		}
		if (n == 1) { return result }
	}

	// n (≠ 1) は、素数
	result += 1
	return result
}

//print(numPrimeFactors(N))
print(Int(ceil(log2(Double(numPrimeFactors(N))))))
