from sys import stdin
import numpy

stdin.readline()

# 工場の座標: shape (N, 2) の2次元配列
factories = numpy.fromstring(stdin.read(), sep=' ').reshape(-1, 2)

# 発電所の座標: 長さ 2 の配列
plant = numpy.median(factories, axis=0)

ans = int(numpy.abs(factories - plant).sum())

print(ans)
