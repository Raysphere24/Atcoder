#include <iostream>

using namespace std;

class SearchState
{
	char chars[24];
	const int total_length;
	int current_position, current_depth;

public:
	SearchState(int n) : total_length(n)
	{
		chars[n] = '\0';

		current_position = 0;
		current_depth = 0;
	}

	void Search()
	{
		if (current_position == total_length) {
			cout << chars << endl;
			return;
		}

		if (current_position + current_depth + 2 <= total_length) {
			chars[current_position] = '(';
			current_position++;
			current_depth++;

			Search();

			current_position--;
			current_depth--;
		}

		if (current_depth > 0) {
			chars[current_position] = ')';
			current_position++;
			current_depth--;

			Search();

			current_position--;
			current_depth++;
		}
	}
};

int main()
{
	int N;
	cin >> N;

	if ((N & 1) == 1) return 0;

	SearchState state(N);

	state.Search();

	return 0;
}
