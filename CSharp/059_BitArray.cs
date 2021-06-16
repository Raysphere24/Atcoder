#nullable enable

using System;
using System.Collections;
//using System.Collections.Generic;

using static System.Console;
using static System.Math;
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
		int numVertices = NextInt();
		int numEdges = NextInt();
		int numRemainingQueries = NextInt();

		(int, int)[] edges = MakeArray(numEdges, i => (NextInt() - 1, NextInt() - 1));

		Array.Sort(edges);

		const int maxConcurrentQueries = 256;

		var bitArrays = MakeArray(numVertices, i => new BitArray(maxConcurrentQueries));
		var goals = new int[maxConcurrentQueries];

		while (numRemainingQueries > 0) {
			foreach (var b in bitArrays)
				b.SetAll(false);

			int numConcurrentQueries = Min(maxConcurrentQueries, numRemainingQueries);

			foreach (int i in Range(0, numConcurrentQueries)) {
				int a = NextInt() - 1;
				int b = NextInt() - 1;
				bitArrays[a].Set(i, true);
				goals[i] = b;
			}

			foreach (var (a, b) in edges) {
				bitArrays[b].Or(bitArrays[a]);
			}

			foreach (int i in Range(0, numConcurrentQueries)) {
				int goal = goals[i];
				WriteLine(bitArrays[goal].Get(i) ? "Yes" : "No");
			}

			numRemainingQueries -= maxConcurrentQueries;
		}
	}
}
