#nullable enable

//using System;
using System.Collections.Generic;

using static System.Console;
//using static System.Math
using static System.Linq.Enumerable;

public class Program
{
	private static (int, int) ReadIntPair()
	{
		var s = ReadLine()!.Split();
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		var (H, W) = ReadIntPair();
		var (rs, cs) = ReadIntPair();
		var (rt, ct) = ReadIntPair();

		rs--; cs--; rt--; ct--;

		var M = new int[H, W];

		foreach (int i in Range(0, H)) {
			string s = ReadLine()!;
			foreach (int j in Range(0, W)) {
				M[i, j] = s[j] == '.' ? int.MaxValue : -1;
			}
		}

		var queue = new Queue<(int, int)>();

		M[rs, cs] = -1;
		queue.Enqueue((rs, cs));

		// 壁に当たるかターン数の小さいセルに当たるまでまっすぐ進み、ターン数を割り当ててキューに追加する
		// 同じターン数ならそのまま進む
		void Run(int r, int c, int turns, int dr, int dc)
		{
			r += dr;
			c += dc;

			while (0 <= r && r < H && 0 <= c && c < W && M[r, c] >= turns) {
				M[r, c] = turns;
				queue.Enqueue((r, c));
				r += dr;
				c += dc;
			}
		}

		while (true) {
			var (r, c) = queue.Dequeue();

			if (r == rt && c == ct) break;

			// 1つ大きいターン数で4方位に進もうと試みる
			int turns = M[r, c] + 1;

			// Run 関数は行き先のセルに turns より小さいターン数がすでに割り当て済みなら何もしない
			Run(r, c, turns, 1, 0);
			Run(r, c, turns, -1, 0);
			Run(r, c, turns, 0, 1);
			Run(r, c, turns, 0, -1);
		}

		WriteLine(M[rt, ct]);
	}
}
