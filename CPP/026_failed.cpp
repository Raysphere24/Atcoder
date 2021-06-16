#include <iostream>
#include <unordered_set>
#include <vector>
#include <boost/algorithm/cxx11/any_of.hpp>
#include <boost/range/irange.hpp>
#include <boost/range/algorithm.hpp>

using namespace std;

struct Vertex
{
	vector<Vertex*> neighbors;
	int label;
};

bool operator<(const Vertex& a, const Vertex& b)
{
	return a.neighbors.size() < b.neighbors.size();
}

class SearchState
{
	vector<Vertex> vertices;
	unordered_set<Vertex*> hashset;
	int N, M;

public:
	SearchState(int N) : vertices(N), N(N), M(N / 2)
	{
		for (int i : boost::irange(0, N)) {
			vertices[i].label = i + 1;
		}
	}

	void add_edge(int tail, int head)
	{
		vertices[tail].neighbors.push_back(&vertices[head]);
		vertices[head].neighbors.push_back(&vertices[tail]);
	}

	void search()
	{
		boost::range::sort(vertices);

		// for (Vertex& v : vertices) {
		// 	cout << v.neighbors.size() << ' ';
		// }

		if (!search_recursive(0)) abort();
		// search_recursive(0);

		for (Vertex* v : hashset) {
			cout << v->label << ' ';
		}
	}

	void nonrecursive_search()
	{
		boost::range::sort(vertices);

		for (Vertex& vertex : vertices) {
			if (boost::algorithm::any_of(vertex.neighbors,
			[&](Vertex* x) -> bool { return hashset.count(x); })) continue;

			hashset.insert(&vertex);

			if (hashset.size() == M) break;
		}

		if (hashset.size() != M) abort();

		for (Vertex* v : hashset) {
			cout << v->label << ' ';
		}
	}

private:
	bool search_recursive(int position)
	{
		if (hashset.size() == M) return true;

		if (position == N) return false;
		// if (M - hashset.size() > N - position) return false;

		Vertex& vertex = vertices[position];

		if (!boost::algorithm::any_of(vertex.neighbors,
			[&](Vertex* x) -> bool { return hashset.count(x); })) {
			hashset.insert(&vertex);

			if (search_recursive(position + 1)) return true;

			// hashset.erase(&vertex);
		}

		else if (search_recursive(position + 1)) return true;

		return false;
	}
};

int main()
{
	int N;
	cin >> N;

	SearchState state(N);

	for (int i : boost::irange(0, N - 1)) {
		int a, b;
		cin >> a >> b;
		state.add_edge(a - 1, b - 1);
	}

	// state.search();
	state.nonrecursive_search();

	return 0;
}
