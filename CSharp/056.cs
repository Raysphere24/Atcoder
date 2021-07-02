#nullable enable

//using System;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

public class Program
{
	private static (int, int) ReadIntPair()
	{
		var s = ReadLine()!.Split();
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		var (N, S) = ReadIntPair();

		var A = new int[N];
		var B = new int[N];

		foreach (int i in Range(0, N)) {
			var (a, b) = ReadIntPair();
			A[i] = a;
			B[i] = b;
		}

		var visited = new HashSet<int>();

		string? Search(int n, int s)
		{
			if (s < 0) return null;

			if (n == 0) return s == 0 ? "" : null;

			if (!visited.Add(n + N * s)) return null;

			string? a = Search(n - 1, s - A[n - 1]);
			if (a != null) return a + "A";

			string? b = Search(n - 1, s - B[n - 1]);
			if (b != null) return b + "B";

			return null;
		}

		WriteLine(Search(N, S) ?? "Impossible");
	}
}
