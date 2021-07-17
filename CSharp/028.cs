using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
//using static System.Math;
using static System.Linq.Enumerable;

public static class Program
{
	private const int S = 1000;

	public static void Main()
	{
		int N = int.Parse(ReadLine());

		var A = new int[S + 1, S + 1];

		foreach (int _ in Range(0, N)) {
			string[] s = ReadLine().Split();
			int lx = int.Parse(s[0]);
			int ly = int.Parse(s[1]);
			int rx = int.Parse(s[2]);
			int ry = int.Parse(s[3]);

			A[lx, ly]++;
			A[lx, ry]--;
			A[rx, ly]--;
			A[rx, ry]++;
		}

		foreach (int i in Range(0, S + 1)) {
			foreach (int j in Range(0, S)) {
				A[i, j + 1] += A[i, j];
			}
			if (A[i, S] != 0) throw new Exception();
		}

		foreach (int j in Range(0, S + 1)) {
			foreach (int i in Range(0, S)) {
				A[i + 1, j] += A[i, j];
			}
			if (A[S, j] != 0) throw new Exception();
		}

		var count = new int[N + 1];

		foreach (int x in A) {
			count[x]++;
		}

		Write(string.Join('\n', count.Skip(1)));
	}
}
