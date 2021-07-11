#include <iostream>

using namespace std;

static const uint64_t m = 1000000007;

// computes a ** n % m
uint64_t pow_mod_m(uint64_t a, uint64_t n) {
	uint64_t r = 1;

	while (n) {
		if (n & 1) r = (r * a) % m;
		a = (a * a) % m;
		n >>= 1;
	}

	return r;
}

int main() {
	uint64_t N, K;
	cin >> N >> K;

	uint64_t ans = K;

	if (N > 1) {
		ans *= K - 1;
		ans %= m;
	}

	if (N > 2) {
		ans *= pow_mod_m(K - 2, N - 2);
		ans %= m;
	}

	cout << ans << endl;
}
