using System.Collections.Generic;

using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		var I = ReadLine().Split(' ').Select(int.Parse).ToList();
		var A = ReadLine().Split(' ').Select(int.Parse).ToList();
		var B = ReadLine().Split(' ').Select(int.Parse).ToList();

		int N = I[0], K = I[1];

		var C = A.Zip(B, (a, b) => Abs(b - a)).ToList();

		int d = C.Sum();
		int parity = C.Aggregate(0, (a, b) => a ^ (b & 1));

		WriteLine(d <= K && parity == (K & 1) ? "Yes" : "No");
	}
}
