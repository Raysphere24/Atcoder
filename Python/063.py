from sys import stdin
import numpy

H, W = map(int, stdin.readline().split())

P = numpy.fromstring(stdin.read(), dtype=int, sep=' ').reshape(H, W)

stack = numpy.ndarray((H, W), dtype=int)

def search(currentRowIndex: int, assignedRows: int) -> int:
	if currentRowIndex == H:
		if assignedRows == 0:
			return 0

		count = numpy.bincount(stack[assignedRows - 1])

		if len(count) == 1:
			return 0

		return count[1:].max() * assignedRows

	a = search(currentRowIndex + 1, assignedRows)

	if assignedRows == 0:
		stack[0] = P[currentRowIndex]
	else:
		prevStackRow = stack[assignedRows - 1]
		stack[assignedRows] = numpy.where(
			prevStackRow == P[currentRowIndex],
			prevStackRow,
			0)

	b = search(currentRowIndex + 1, assignedRows + 1)

	return max(a, b)

print(search(0, 0))
