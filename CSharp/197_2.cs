using static System.Console;
using static System.Linq.Enumerable;

class Program
{
	static void Main()
	{
		var I = ReadLine().Split(' ').Select(int.Parse).ToArray();
		int H = I[0], W = I[1], X = I[2] - 1, Y = I[3] - 1;

		var S = new string[H];

		foreach (int i in Range(0, H))
			S[i] = ReadLine();

		if (S[X][Y] == '#') {
			WriteLine(0);
			return;
		}

		int a, b, c, d;
		for (a = X; a >= 0 && S[a][Y] == '.'; a--) ;

		for (b = X; b < H && S[b][Y] == '.'; b++) ;

		for (c = Y; c >= 0 && S[X][c] == '.'; c--) ;

		for (d = Y; d < W && S[X][d] == '.'; d++) ;

		WriteLine(b - a + d - c - 3);
	}
}
