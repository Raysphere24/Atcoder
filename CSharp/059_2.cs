#nullable enable

using System;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

using static Scanner;
using static Utils;

static class Scanner
{
	private static string[]? s;
	private static int i;

	public static string NextToken()
	{
		if (s == null || i == s.Length) {
			s = ReadLine()!.Split();
			i = 0;
		}

		return s[i++];
	}

	public static int NextInt() => int.Parse(NextToken());
	public static long NextLong() => long.Parse(NextToken());
	public static double NextDouble() => double.Parse(NextToken());
}

static class Utils
{
	public static T[] ReadArray<T>(Func<string, T> selector)
		=> ReadLine()!.Split().Select(selector).ToArray();

	public static T[] MakeArray<T>(int length, Func<int, T> selector)
		=> Range(0, length).Select(selector).ToArray();
}

public class Program
{
	public static void Main()
	{
		int N = NextInt();
		int M = NextInt();
		int Q = NextInt();

		(int, int)[] edges = MakeArray(M, i => (NextInt() - 1, NextInt() - 1));

		Array.Sort(edges);

		var bits = new ulong[N];
		var goals = new List<int>(64);

		while (Q > 0) {
			Array.Fill(bits, 0UL);
			goals.Clear();

			ulong bit = 1UL;
			while (bit > 0 && Q > 0) {
				int a = NextInt() - 1;
				int b = NextInt() - 1;
				bits[a] |= bit;
				goals.Add(b);

				bit <<= 1;
				Q--;
			}

			foreach (var (a, b) in edges) {
				bits[b] |= bits[a];
			}

			bit = 1;
			foreach (int goal in goals) {
				WriteLine((bits[goal] & bit) != 0 ? "Yes" : "No");
				bit <<= 1;
			}
		}
	}
}
