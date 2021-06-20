#nullable enable

using System;
using System.Collections.Generic;

using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

public class Program
{
	const int M = 200000;

	public static void Main()
	{
		int N = int.Parse(ReadLine()!);

		int[] A = ReadLine()!.Split().Select(int.Parse).ToArray();

		var adjacency = Range(0, M).Select(i => new List<int>()).ToArray();

		foreach (int i in Range(0, N / 2)) {
			int x = A[i] - 1;
			int y = A[N - 1 - i] - 1;
			if (x == y) continue;
			adjacency[x].Add(y);
			adjacency[y].Add(x);
		}

		var visited = new HashSet<int>(M);

		int CountConnected(int x) => !visited.Add(x) ? 0 : 1 + adjacency[x].Sum(CountConnected);

		WriteLine(Range(0, M).Sum(i => Max(CountConnected(i) - 1, 0)));
	}
}
