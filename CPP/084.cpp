#include <iostream>

using namespace std;

int main() {
	uint64_t N;
	cin >> N;

	char s[1000001];
	cin >> s;

	uint64_t prev_i = 0;
	uint64_t ans = 0;
	for (uint64_t i = 1; i <= N; i++) {
		// i == N のとき s[i] == '\0' なので常に成立する
		if (s[i - 1] != s[i]) {
			ans += prev_i * (i - prev_i);
			prev_i = i;
		}
	}

	cout << ans;
}
