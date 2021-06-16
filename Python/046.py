import sys, numpy

sys.stdin.readline()

A = numpy.fromstring(sys.stdin.readline(), dtype=int, sep=' ')
B = numpy.fromstring(sys.stdin.readline(), dtype=int, sep=' ')
C = numpy.fromstring(sys.stdin.readline(), dtype=int, sep=' ')

m = 46

a = numpy.bincount(A % m, minlength=m)
b = numpy.bincount(B % m, minlength=m)
c = numpy.bincount(C % m, minlength=m)

print(sum(a[i] * b[j] * c[-(i+j) % m] for i, j in numpy.ndindex(m, m)))
