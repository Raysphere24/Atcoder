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

// N の素因数の数を求める
func numPrimeFactors(_ N: UInt) -> Int {
	var result = N.trailingZeroBitCount
	var n = N >> result
	if (n == 1) { return result }

	var p: UInt = 3
	while p * p <= n {
		while (n % p == 0) {
			result += 1
			n /= p
		}
		if (n == 1) { return result }
		p += 2
	}

	// 残った因数は、素数
	result += 1
	return result
}

//print(numPrimeFactors(N))
//print(Int(ceil(log2(Double(numPrimeFactors(N))))))
print(N |> numPrimeFactors |> Double.init |> log2 |> ceil |> Int.init)
