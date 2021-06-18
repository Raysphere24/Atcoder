#nullable enable

//using System;
//using System.Collections.Generic;

using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

public class Program
{
	private static (int, int) ReadIntPair()
	{
		string[] s = ReadLine()!.Split();
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		int N = int.Parse(ReadLine()!);

		(int X, int Y)[] factories = Range(0, N).Select(i => ReadIntPair()).ToArray();

		long x = factories.Select(p => p.X).OrderBy(x => x).ElementAt(N / 2);
		long y = factories.Select(p => p.Y).OrderBy(y => y).ElementAt(N / 2);

		long ans = factories.Sum(p => Abs(p.X - x) + Abs(p.Y - y));

		WriteLine(ans);
	}
}
