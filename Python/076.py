from sys import stdin

stdin.readline()
A = list(map(int, stdin.readline().split()))

def search():
	K, mod = divmod(sum(A), 10)
	if mod != 0: return "No"

	i = 0
	j = 0

	# A[i] から A[j - 1] までの和
	s = 0

	# A[-n] で A の末尾から n 番目の要素を表す性質を使って、負の i から始める
	while s < K:
		i -= 1
		s += A[i]

	while j < len(A):
		if s == K: return "Yes"

		if s > K:
			s -= A[i]
			i += 1
		else:
			s += A[j]
			j += 1

	return "No"

print(search())
