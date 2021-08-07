using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

public static class Program
{
	public static void Main()
	{
		string[] s = ReadLine().Split();
		int N = int.Parse(s[0]);
		int K = int.Parse(s[1]);

		int[] c = ReadLine().Split().Select(int.Parse).ToArray();

		var count = new Dictionary<int, int>();

		void Add(int x)
		{
			if (count.ContainsKey(x))
				count[x]++;
			else
				count[x] = 1;
		}

		void Remove(int x)
		{
			if (count[x] > 1)
				count[x]--;
			else
				count.Remove(x);
		}

		int i = 0;

		while (i < K) {
			Add(c[i++]);
		}

		int ans = count.Count;

		while (i < N) {
			Remove(c[i - K]);
			Add(c[i++]);
			ans = Max(ans, count.Count);
		}

		WriteLine(ans);
	}
}
