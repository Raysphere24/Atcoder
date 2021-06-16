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

		var partial_sum = new int[2, N];
		var current_sum = new int[2];

		foreach (int i in Range(0, N)) {
			var (C, P) = ReadIntPair();

			current_sum[C - 1] += P;

			foreach (int j in Range(0, 2)) {
				partial_sum[j, i] = current_sum[j];
			}
		}

		int Q = ReadInt();

		foreach (int i in Range(0, Q)) {
			var (L, R) = ReadIntPair();

			foreach (int j in Range(0, 2)) {
				int sum = partial_sum[j, R - 1];
				if (L > 1) {
					sum -= partial_sum[j, L - 2];
				}
				Write(sum);
				Write(' ');
			}
			WriteLine();
		}
	}
}
