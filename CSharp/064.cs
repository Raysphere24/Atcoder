#nullable enable

using System;
using System.Collections.Generic;

using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

using static Scanner;

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

	public static T[] ReadArray<T>(Func<string, T> selector)
		=> ReadLine()!.Split().Select(selector).ToArray();
}

public static class Program
{
	public static void Main()
	{
		int N = NextInt();
		int Q = NextInt();

		long[] A = ReadArray(long.Parse);

		long[] D = Range(0, N - 1).Select(i => A[i + 1] - A[i]).ToArray();

		long ans = D.Sum(d => Abs(d));

		foreach (int _ in Range(0, Q)) {
			int L = NextInt() - 2;
			int R = NextInt() - 1;
			long V = NextLong();

			if (L >= 0) {
				ans -= Abs(D[L]);
				D[L] += V;
				ans += Abs(D[L]);
			}

			if (R < N - 1) {
				ans -= Abs(D[R]);
				D[R] -= V;
				ans += Abs(D[R]);
			}

			WriteLine(ans);
		}
	}
}
