using System;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

public class Program
{
	static (int, int) ReadIntPair()
	{
		var s = ReadLine().Split(' ');
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		var (N, K) = ReadIntPair();

		var C = new long[2 * N];
		// var C = new List<long>(2 * N);

		foreach (int i in Range(0, N)) {
			var (a, b) = ReadIntPair();

			C[2 * i] = b;
			C[2 * i + 1] = a - b;

			// C.Add(b);
			// C.Add(a - b);
		}

		Array.Sort(C);
		// C.Sort();

		WriteLine(C.TakeLast(K).Sum());
	}
}
