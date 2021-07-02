#nullable enable

using System.Linq;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

public class Program
{
	private static List<int> CalcLengthsOfIncreasingSubsequence(List<int> A)
	{
		var B = new List<int>();
		var result = new List<int>(A.Count);

		foreach (int x in A) {
			int pos = B.BinarySearch(x);

			if (pos < 0) {
				pos = ~pos;

				if (pos == B.Count)
					B.Add(x);
				else
					B[pos] = x;
			}

			result.Add(B.Count);
		}

		return result;
	}

	public static void Main()
	{
		ReadLine();
		List<int> A = ReadLine()!.Split().Select(int.Parse).ToList();

		List<int> P = CalcLengthsOfIncreasingSubsequence(A);

		A.Reverse();

		List<int> Q = CalcLengthsOfIncreasingSubsequence(A);

		Q.Reverse();

		WriteLine(Enumerable.Zip(P, Q, (p, q) => p + q).Max() - 1);
	}
}
