from sys import stdin
from math import hypot, atan2, cos, sin, pi

T = float(stdin.readline())
L, X, Y = map(float, stdin.readline().split(' '))
Q = int(stdin.readline())
R = L / 2

for i in range(Q):
	e = float(stdin.readline())
	theta = 2 * pi * e / T
	y = -R * sin(theta)
	z = R - R * cos(theta)
	d = hypot(X, Y - y)
	print(atan2(z, d) * 180 / pi)
