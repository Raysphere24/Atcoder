using static System.Console;
using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		int N = int.Parse(ReadLine());
		int[] A = ReadLine().Split(' ').Select(int.Parse).ToArray();
		int min_xor = int.MaxValue;

		foreach (int splitter in Range(0, 1 << (N - 1))) {
			int xor = 0, or = A[0];
			Write(A[0]);

			for (int i = 1; i < N; i++) {
				bool split = (splitter & (1 << (i - 1))) != 0;
				int num = A[i];

				if (split) {
					xor ^= or;
					or = 0;
				}

				Write(split ? '|' : ' ');
				Write(num);

				or |= num;
			}

			xor ^= or;

			Write(':');
			WriteLine(xor);

			if (min_xor > xor)
				min_xor = xor;
		}

		WriteLine(min_xor);
	}
}
