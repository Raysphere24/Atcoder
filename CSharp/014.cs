using System.Linq;

using static System.Console;
using static System.Math;
//using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		ReadLine();

		var A = ReadLine().Split().Select(long.Parse).ToList();
		var B = ReadLine().Split().Select(long.Parse).ToList();

		A.Sort();
		B.Sort();

		// どちらでも OK
		//WriteLine(A.Zip(B, (a, b) => Abs(a - b)).Sum());
		WriteLine(Enumerable.Zip(A, B, (a, b) => Abs(a - b)).Sum());
	}
}
