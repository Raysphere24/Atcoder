#include <iostream>
#include <cmath>
#include <queue>
#include <vector>

#include <boost/range/irange.hpp>

using namespace std;
using namespace boost;

struct Vertex;

struct Connection {
	Vertex *target;
	long c, d, minTime;

	Connection(Vertex *target, long c, long d) : target(target), c(c), d(d) {
//		long t = (long)sqrt(d);
//		minTime = (t > 1 && calcTime(t - 1) < calcTime(t)) ? t - 1 : t;
		minTime = (long)round(sqrt(d) - 1);
	}

	long calcTime(long time) const { return time + c + d / (time + 1); }
};

struct Vertex {
	vector<Connection> connections;
	long distance = -1;
};

struct SearchNode {
	Vertex *target;
	long time;
};

bool operator <(const SearchNode &a, const SearchNode &b) { return a.time > b.time; }

int main() {
	long N, M;
	cin >> N >> M;

	Vertex vertices[N];

	for (long i : irange(M)) {
		long a, b, c, d;
		cin >> a >> b >> c >> d;
		a--; b--;

		vertices[a].connections.emplace_back(&vertices[b], c, d);
		vertices[b].connections.emplace_back(&vertices[a], c, d);
	}

	Vertex *goal = &vertices[N - 1];

	priority_queue<SearchNode> queue;

	queue.push({&vertices[0], 0L});

	while (!queue.empty()) {
		auto [target, time] = queue.top();
		queue.pop();

		if (target->distance >= 0) continue;
		target->distance = time;

		if (target == goal) break;

		for (const Connection &connection : target->connections) {
			if (connection.target->distance >= 0) continue;
			long t = max(connection.minTime, time);
			queue.push({connection.target, connection.calcTime(t)});
		}
	}

	cout << goal->distance << endl;

	return 0;
}
