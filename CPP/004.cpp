#include <iostream>
#include <string>
#include <vector>

#include <boost/range/irange.hpp>

using namespace std;

int main() {
	uint H, W;
	cin >> H >> W;

	uint A[H][W];
	vector<uint> R(H, 0), C(W, 0);

	for (uint r : boost::irange(H)) {
		for (uint c : boost::irange(W)) {
			uint a;
			cin >> a;
			A[r][c] = a;
			R[r] += a;
			C[c] += a;
		}
	}

	string output;

	for (uint r : boost::irange(H)) {
		for (uint c : boost::irange(W)) {
			output += to_string(R[r] + C[c] - A[r][c]);
			output += ' ';
		}
		output += '\n';
	}

	cout << output;

	return 0;
}
