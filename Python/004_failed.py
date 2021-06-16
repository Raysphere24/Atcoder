import sys, numpy

H, W = map(int, sys.stdin.readline().split(' '))

A = numpy.fromstring(sys.stdin.read(), dtype=int, sep=' ').reshape(H, W)

R = A.sum(axis=0)
C = A.sum(axis=1)

B = R + C[:, numpy.newaxis] - A

for row in B:
	print(" ".join(map(str, row)))
