#include <iostream>
#include <cstdint>
#include <vector>

#include <boost/range/algorithm/sort.hpp>
#include <boost/range/algorithm/fill.hpp>

using namespace std;

int main() {
	uint N, M, Q;
	cin >> N >> M >> Q;

	vector<pair<uint, uint>> edges(M);

	for (auto &e : edges) {
		uint x, y;
		cin >> x >> y;
		e = make_pair(x - 1, y - 1);
	}

	boost::sort(edges);

	vector<uint64_t> bits(N);
	vector<uint> goals;
	goals.reserve(64);

	while (Q > 0) {
		boost::fill(bits, 0UL);
		goals.clear();

		uint64_t bit = 1UL;
		while (bit > 0 && Q > 0) {
			uint a, b;
			cin >> a >> b;
			bits[a - 1] |= bit;
			goals.push_back(b - 1);

			bit <<= 1;
			Q--;
		}

		for (auto [a, b] : edges) {
			bits[b] |= bits[a];
		}

		bit = 1UL;
		for (uint goal : goals) {
			cout << (bits[goal] & bit ? "Yes" : "No") << endl;
			bit <<= 1;
		}
	}

	return 0;
}
