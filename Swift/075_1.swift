import Foundation

// 括弧が重なって見にくいため、パイプ演算子を定義 (F# style)
precedencegroup ForwardPipe {
	associativity: left
}

infix operator |> : ForwardPipe

func |> <T, U>(value: T, function: ((T) -> U)) -> U {
	return function(value)
}

let N = UInt(readLine()!)!

// √N 以下の奇素数のリスト
var oddPrimes: [UInt] = []

// 3 以上 √N 以下の奇数でループ
outerLoop: for i in stride(from: 3, through: (N |> Double.init |> sqrt |> UInt.init), by: 2) {
	for p in oddPrimes {
		if p * p > i { break } // i は素数
		if i % p == 0 { continue outerLoop } // i は素数でない
	}
	oddPrimes.append(i)
}

// N の素因数の数を求める
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

//print(Int(ceil(log2(Double(numPrimeFactors(N))))))
print(N |> numPrimeFactors |> Double.init |> log2 |> ceil |> Int.init)
