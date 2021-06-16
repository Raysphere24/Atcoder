#nullable enable

//using System;
using System.Collections.Generic;

using static System.Console;
//using static System.Math
using static System.Linq.Enumerable;

public class Program
{
	private static (int, int) ReadIntPair()
	{
		var s = ReadLine()!.Split(' ');
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		var (N, M) = ReadIntPair();

		var pawns = new List<(int x, int y)>(M);

		foreach (int _ in Range(0, M)) {
			pawns.Add(ReadIntPair());
		}

		pawns.Sort();

		var set = new HashSet<int> { N };

		(int x, int y) prev = (0, N);
		bool prev_contained = false;

		foreach (var p in pawns) {
			bool contained = set.Contains(p.y);
			set.Remove(p.y);

			if (prev.x == p.x && prev.y == p.y - 1 ? prev_contained : set.Contains(p.y - 1))
				set.Add(p.y);

			if (set.Contains(p.y + 1))
				set.Add(p.y);

			prev = p;
			prev_contained = contained;
		}

		WriteLine(set.Count);
	}
}
