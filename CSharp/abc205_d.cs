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
		int Q = NextInt();

		long[] A = ReadArray(long.Parse);

		var B = new List<long>();
		var C = new List<long>();

		int i = 0;
		long b = A[0];
		while (i < N) {
			while (i < N - 1 && A[i + 1] - A[i] == 1) { i++; }

			//WriteLine($"{i}, {A[i]}, {b}, {A[i] + 1}");

			B.Add(b);
			C.Add(A[i] + 1);

			i++;
			if (i < N) b += A[i] - A[i - 1] - 1;
		}

		//WriteLine(string.Join(", ", B));
		//WriteLine(string.Join(", ", C));

		foreach (int _ in Range(0, Q)) {
			long K = NextLong();

			int pos = B.BinarySearch(K);

			if (pos == -1) {
				WriteLine(K);
			}
			else {
				if (pos < 0) pos = -2 - pos;

				WriteLine(C[pos] - B[pos] + K);
			}
		}
	}
}
