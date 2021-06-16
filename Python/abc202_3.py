import sys, numpy

sys.stdin.readline()

A = numpy.fromstring(sys.stdin.readline(), dtype=int, sep=' ') - 1
B = numpy.fromstring(sys.stdin.readline(), dtype=int, sep=' ') - 1
C = numpy.fromstring(sys.stdin.readline(), dtype=int, sep=' ') - 1

D = numpy.bincount(A, minlength=len(A))

print(sum(D[B[C]]))
