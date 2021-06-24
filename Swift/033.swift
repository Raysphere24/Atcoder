func readInt2() -> (Int, Int) {
	let s = readLine()!.split(separator: " ")
	return (Int(s[0])!, Int(s[1])!)
}

let (H, W) = readInt2()

print(H == 1 || W == 1 ? H * W : ((H + 1) / 2) * ((W + 1) / 2))
