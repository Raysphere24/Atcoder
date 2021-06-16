using System;

using static System.Console;
using static System.Linq.Enumerable;

public static class Program
{
	static (int, int) ReadIntPair()
	{
		var s = ReadLine().Split(' ');
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		var (H, W) = ReadIntPair();

		var A = new int[W, H];

		foreach (int y in Range(0, H)) {
			string s = ReadLine();
			foreach (int x in Range(0, W)) {
				A[x, y] = s[x] == '+' ? 1 : -1;
			}
		}

		var B = new int[W + 1, H + 1];

		foreach (int y in Range(0, H))
			B[W, y] = int.MaxValue;

		foreach (int x in Range(0, W))
			B[x, H] = int.MaxValue;

		B[W - 1, H - 1] = A[W - 1, H - 1];

		for (int i = W + H - 3; i >= 0; i--) {
			foreach (int x in Range(0, W)) {
				int y = i - x;
				if (y < 0 || y >= H) continue;

				B[x, y] = A[x, y] - Math.Min(B[x + 1, y], B[x, y + 1]);
			}
		}

		int score = A[0, 0] - B[0, 0];

		if ((W + H) % 2 == 1) score = -score;

        //WriteLine(score);
        WriteLine(score > 0 ? "Takahashi" : score < 0 ? "Aoki" : "Draw");
    }
}
