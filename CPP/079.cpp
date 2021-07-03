#include <iostream>

#include <boost/range/irange.hpp>

using namespace std;

int main() {
	int H, W;
	cin >> H >> W;

	int A[H][W], B[H][W];

	for (auto &r : A) for (int &x : r) cin >> x;
	for (auto &r : B) for (int &x : r) cin >> x;

	long ans = 0;

	for (int r : boost::irange(H - 1)) {
		for (int c : boost::irange(W - 1)) {
			int d = B[r][c] - A[r][c];
			A[r][c] += d;
			A[r + 1][c] += d;
			A[r][c + 1] += d;
			A[r + 1][c + 1] += d;
			ans += abs(d);
		}

		if (A[r][W - 1] != B[r][W - 1]) {
			cout << "No" << endl;
			return 0;
		}
	}

	for (int c : boost::irange(W)) {
		if (A[H - 1][c] != B[H - 1][c]) {
			cout << "No" << endl;
			return 0;
		}
	}

	cout << "Yes" << endl;
	cout << ans << endl;

	return 0;
}
