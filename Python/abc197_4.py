import sys, cmath

N = int(sys.stdin.readline())
(ax, ay) = sys.stdin.readline().split()
(bx, by) = sys.stdin.readline().split()

a = float(ax) + float(ay) * 1j
b = float(bx) + float(by) * 1j

center = (a + b) / 2
rotation = cmath.exp(2j * cmath.pi / N)
c = center + rotation * (a - center)

print(c.real, c.imag)
