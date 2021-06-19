#include <iostream>
#include <cstdint>

#include <boost/range/irange.hpp>

using namespace std;

uint64_t f(uint64_t x) {
	uint64_t ans = 0, power = 1;

	while (x > 0) {
		uint64_t d = x % 9;

		if (d == 8) d = 5;

		ans += d * power;

		x /= 9;
		power *= 8;
	}

	return ans;
}

int main() {
	uint64_t N, K;
	cin >> oct >> N >> dec >> K;

	for (uint i : boost::irange(K)) {
		N = f(N);
	}

	cout << oct << N << endl;

	return 0;
}
