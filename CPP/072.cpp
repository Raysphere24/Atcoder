#include <iostream>

#include <boost/range/irange.hpp>

using namespace std;
using namespace boost;

enum Movement { None, Left, Right, Up, Down };

class SearchState {
	int H, W;
	bool wall[16][16];
	bool visited[16][16] = {};
	int start_r, start_c;
	int current_max = -1;

public:
	void read_from_console() {
		cin >> H >> W;

		for (int i : irange(H)) {
			char s[17];
			cin >> s;
			for (int j : irange(W)) {
				wall[i][j] = s[j] == '#';
			}
		}
	}

	int search() {
		for (int i : irange(H)) {
			for (int j : irange(W)) {
				if (!wall[i][j]) {
					start_r = i;
					start_c = j;
					search(i, j, 0, None);
				}
			}
		}

		return current_max;
	}

private:
	void search(int r, int c, int distance, Movement movement) {
		if (distance > 0 && r == start_r && c == start_c) {
			current_max = max(current_max, distance);
			return;
		}

		if (r < 0 || r >= H || c < 0 || c >= W || wall[r][c] || visited[r][c]) return;

		visited[r][c] = true;

		int d = distance + 1;
		if (movement != Left) {
			search(r, c + 1, d, Right);
		}
		if (movement != Right) {
			search(r, c - 1, d, Left);
		}
		if (movement != Up) {
			search(r + 1, c, d, Down);
		}
		if (movement != Down) {
			search(r - 1, c, d, Up);
		}

		visited[r][c] = false;
	}
};

int main() {
	SearchState state;
	state.read_from_console();
	cout << state.search() << endl;
	return 0;
}