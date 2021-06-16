#include <iostream>
#include <cstdint>

using namespace std;

uint64_t calc(uint64_t P, uint64_t Q, uint64_t A[], uint64_t N, uint64_t remaining, uint64_t product_mod_P) {
	if (remaining == 0) {
		return product_mod_P == Q ? 1 : 0;
	}

	uint64_t result = 0;
	for (uint64_t i = remaining - 1; i < N; i++) {
		result += calc(P, Q, A, i, remaining - 1, (product_mod_P * A[i]) % P);
	}

	return result;
}

int main() {
	uint64_t N, P, Q;
	cin >> N >> P >> Q;

	uint64_t A[N];
	for (uint64_t &a : A) {
		cin >> a;
	}

	cout << calc(P, Q, A, N, 5, 1) << endl;

	return 0;
}
