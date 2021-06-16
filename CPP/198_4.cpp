#include <iostream>
#include <vector>
#include <algorithm>

using uchar = unsigned char;
using ulong = unsigned long;

struct Variable
{
	char name;
	uchar index;
	uchar min;
};

class SearchState
{
	std::vector<Variable> variables;
	std::vector<uchar> A, B, C;

	uchar values[10] = {};
	bool is_used[10] = {};

public:
	SearchState(char* a, char* b, char* c)
	{
		assign_variables(A, a);
		assign_variables(B, b);
		assign_variables(C, c);
	}
	void assign_variables(std::vector<uchar>& expr, char* s)
	{
		uchar min = 1;

		while (char c = *s) {
			auto i = std::find_if(
				variables.begin(),
				variables.end(),
				[c](Variable& v) { return v.name == c; });

			if (i != variables.end()) {
				i->min |= min;
				expr.push_back(i->index);
			}
			else {
				Variable v = { c, variables.size(), min };
				variables.push_back(v);
				expr.push_back(v.index);
			}
			s++;
			min = 0;
		}
	}
	bool is_invalid()
	{
		return variables.size() > 10;
	}
	ulong eval(std::vector<uchar>& expr)
	{
		ulong result = 0;

		for (uchar index : expr) {
			result *= 10;
			result += values[index];
		}

		return result;
	}
	bool search(uchar n_assigned)
	{
		if (n_assigned == variables.size()) {
			return eval(A) + eval(B) == eval(C);
		}

		auto& v = variables[n_assigned];
		for (uchar i = v.min; i <= 9; i++) {
			if (is_used[i]) continue;

			values[n_assigned] = i;
			is_used[i] = true;

			if (search(n_assigned + 1))
				return true;

			is_used[i] = false;
		}

		return false;
	}
	void print()
	{
		std::cout << eval(A) << std::endl;
		std::cout << eval(B) << std::endl;
		std::cout << eval(C) << std::endl;
	}
};

int main()
{
	char a[11], b[11], c[11];

	std::cin >> a >> b >> c;

	SearchState s(a, b, c);

	if (s.is_invalid()) {
		std::cout << "UNSOLVABLE" << std::endl;
		return 0;
	}

	if (s.search(0)) {
		s.print();
	}
	else {
		std::cout << "UNSOLVABLE" << std::endl;
	}

	return 0;
}
