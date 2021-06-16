#include <iostream>
#include <array>
#include <algorithm>

using namespace std;

int search(int N, int A, int B, int C)
{
	int min_f = 1 << 30;

	for (int z = N / C; z >= 0; z--) {
		for (int y = (N - C * z) / B; y >= 0; y--) {
			int numerator = N - C * z - B * y;
			if (numerator % A != 0) continue;

			int x = numerator / A;
			int f = x + y + z;

			min_f = min(min_f, f);
			if (x == 0) return min_f;
		}
	}

	return min_f;
}

int main()
{
	int N;
	array<int, 3> D;

	cin >> N >> D[0] >> D[1] >> D[2];

	sort(D.begin(), D.end());

	cout << search(N, D[0], D[1], D[2]);

	return 0;
}
