import sys, math

(R, X, Y) = map(float, sys.stdin.readline().split())

D = math.hypot(X, Y)

print(math.ceil(D / R) if D >= R else 2)
