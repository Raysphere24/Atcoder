#nullable enable

using System;
using System.Collections.Generic;

using static System.Console;
//using static System.Math
using static System.Linq.Enumerable;

public class Program
{
	public static void Main()
	{
		string[] s = ReadLine()!.Split();
		uint N = uint.Parse(s[0]);
		uint M = uint.Parse(s[1]);
		uint Q = uint.Parse(s[2]);

		List<uint>[] adjacency_lists = Range(0, (int)N)
			.Select(i => new List<uint>()).ToArray();

		foreach (int _ in Range(0, (int)M)) {
			s = ReadLine()!.Split();
			uint x = uint.Parse(s[0]) - 1;
			uint y = uint.Parse(s[1]) - 1;
			adjacency_lists[x].Add(y);
		}

		uint[][] adjacency = adjacency_lists.Select(adj => adj.ToArray()).ToArray();

		var bits = new ulong[N];
		var goals = new List<uint>(64);

		while (Q > 0) {
			Array.Fill(bits, 0UL);
			goals.Clear();

			ulong bit = 1UL;
			while (bit > 0 && Q > 0) {
				s = ReadLine()!.Split();
				uint a = uint.Parse(s[0]) - 1;
				uint b = uint.Parse(s[1]) - 1;
				bits[a] |= bit;
				goals.Add(b);

				bit <<= 1;
				Q--;
			}

			for (uint i = 0; i < N; i++) {
				bit = bits[i];
				foreach (uint neighbor in adjacency[i]) {
					bits[neighbor] |= bit;
				}
			}

			bit = 1;
			foreach (uint goal in goals) {
				WriteLine((bits[goal] & bit) != 0 ? "Yes" : "No");
				bit <<= 1;
			}
		}
	}
}
