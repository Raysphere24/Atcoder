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
		var A = ReadLine().Split().Select(uint.Parse).ToArray();
		var B = new HashSet<uint>(ReadLine().Split().Select(uint.Parse));

		bool IsGood(uint x)
		{
			foreach (uint a in A) {
				if (!B.Contains(a ^ x)) return false;
			}
			return true;
		}

		uint[] X = B.Select(b => A[0] ^ b).Where(IsGood).ToArray();

		Array.Sort(X);

		WriteLine(X.Length);
		WriteLine(string.Join('\n', X));
	}
}
