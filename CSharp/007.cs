using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		int N = int.Parse(ReadLine());

		var A = ReadLine().Split(' ').Select(int.Parse).ToList();

		int Q = int.Parse(ReadLine());

		A.Sort();

		foreach (int _ in Range(0, Q)) {
			int b = int.Parse(ReadLine());

			int pos = A.BinarySearch(b);

			if (pos >= 0) {
				WriteLine(0);
			}
			else {
				int i = Max(-2 - pos, 0);
				int j = Min(-1 - pos, A.Count - 1);

				WriteLine(Min(Abs(A[i] - b), Abs(A[j] - b)));
			}
		}
	}
}
