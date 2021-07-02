#nullable enable

using System.Text;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

public static class Program
{
	public static void Main()
	{
		string[] s = ReadLine()!.Split();
		int H = int.Parse(s[0]);
		int W = int.Parse(s[1]);

		var A = new uint[H, W];
		var R = new uint[H];
		var C = new uint[W];

		for (int r = 0; r < H; r++) {
			s = ReadLine()!.Split();
			for (int c = 0; c < W; c++) {
				uint a = uint.Parse(s[c]);
				A[r, c] = a;
				R[r] += a;
				C[c] += a;
			}
		}

		var output = new StringBuilder();

		for (int r = 0; r < H; r++) {
			for (int c = 0; c < W; c++) {
				output.Append(R[r] + C[c] - A[r, c]);
				output.Append(' ');
			}
			output.AppendLine();
		}

		Write(output);
	}
}
