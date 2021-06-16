using static System.Console;
using static System.Linq.Enumerable;

public class Program
{
	static int ReadInt() => int.Parse(ReadLine());

	static (int, int) ReadIntPair()
	{
		var s = ReadLine().Split(' ');
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		int N = ReadInt();

		var partial_sum = new int[2, N + 1];

		foreach (int i in Range(1, N)) {
			var (C, P) = ReadIntPair();

			foreach (int j in Range(0, 2))
				partial_sum[j, i] = partial_sum[j, i - 1];

			partial_sum[C - 1, i] += P;
		}

		int Q = ReadInt();

		foreach (int i in Range(0, Q)) {
			var (L, R) = ReadIntPair();

			foreach (int j in Range(0, 2)) {
				int sum = partial_sum[j, R] - partial_sum[j, L - 1];
				Write(sum);
				Write(' ');
			}
			WriteLine();
		}
	}
}
