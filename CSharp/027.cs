using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		int N = int.Parse(ReadLine());

		var hashset = new HashSet<string>();

		foreach (int i in Range(1, N)) {
			if (hashset.Add(ReadLine())) {
				WriteLine(i);
			}
		}
	}
}
