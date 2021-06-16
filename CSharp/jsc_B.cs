using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		ReadLine();
		var A = ReadLine().Split(' ').Select(int.Parse);
		var B = ReadLine().Split(' ').Select(int.Parse);

		var C = new List<int>();
		C.AddRange(A.Except(B));
		C.AddRange(B.Except(A));

		C.Sort();
		WriteLine(string.Join(' ', C));
	}
}
