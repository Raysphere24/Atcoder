let N = Int(readLine()!)!

let m = 1000000007

let A = (0 ..< N).map { i in readLine()!.split(separator: " ").map { Int($0)! } }

let answer = A.reduce(1) { acc, list in
	(acc * list.reduce(0, +)) % m
}

print(answer)
