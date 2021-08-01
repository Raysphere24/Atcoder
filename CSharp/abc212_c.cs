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
		ReadLine();
		var A = ReadLine().Split().Select(int.Parse).ToArray();
		var B = ReadLine().Split().Select(int.Parse).ToArray();

		Array.Sort(B);

		int answer = A.Min(a => {
			int index = Array.BinarySearch(B, a);
			if (index >= 0) return 0;
			int i = Max(0, ~index - 1);
			int j = Min(B.Length - 1, ~index);
			return Min(Abs(a - B[i]), Abs(a - B[j]));
		});

		WriteLine(answer);
	}
}
