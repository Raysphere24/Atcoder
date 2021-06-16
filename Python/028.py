import numpy

from sys import stdin

max_x = max_y = 1000

N = int(stdin.readline())

A = numpy.zeros(shape=(max_x + 1, max_y + 1), dtype=int)

for i in range(N):
	lx, ly, rx, ry = map(int, stdin.readline().split(' '))
	A[lx, ly] += 1
	A[rx, ly] -= 1
	A[lx, ry] -= 1
	A[rx, ry] += 1

A.cumsum(axis=0, out=A)
A.cumsum(axis=1, out=A)

counts = numpy.bincount(A.flatten(), minlength=N+1)

print("\n".join(map(str, counts[1:])))
# for count in counts[1:]:
# 	print(count)
