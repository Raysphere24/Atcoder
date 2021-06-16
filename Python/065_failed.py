from sys import stdin
from math import comb

R, G, B, K = map(int, stdin.readline().split())
X, Y, Z = map(int, stdin.readline().split())

# quotas
r = K - Y
g = K - Z
b = K - X

quotas = r + g + b

patterns_quotas = comb(R, r) * comb(G, g) * comb(B, b)
patterns_discretionary = comb(R + G + B - quotas, K - quotas)

print(patterns_quotas * patterns_discretionary % 998244353)
