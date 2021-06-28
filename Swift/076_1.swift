import Foundation

_ = readLine()
var A = readLine()!.split(separator: " ").map { Int($0)! }

func search() -> Bool {
	let sum_of_A = A.reduce(0, +)
	let (K, mod) = sum_of_A.quotientAndRemainder(dividingBy: 10)
	guard mod == 0 else { return false }

	var i = 0
	var j = 0

	var sum = 0

	while sum < K {
		A.append(A[j])
		sum += A[j]
		j += 1
	}

	while j < A.count {
		if (sum == K) { return true }

		if (sum > K) {
			sum -= A[i]
			i += 1
		}
		else {
			sum += A[j]
			j += 1
		}
	}

	return false
}

print(search() ? "Yes" : "No")
