using System;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

public class Program
{
	public static void Main()
	{
		int N = int.Parse(ReadLine());

		if (N == 1) {
			WriteLine(1);
			return;
		}

		var map = new Dictionary<char, uint> { ['R'] = 0, ['G'] = 1, ['B'] = 2 };

		// 事前に R, G, B を 0, 1, 2 に変換しておく
		uint[] S = ReadLine().Select(c => map[c]).ToArray();
		uint[] T = ReadLine().Select(c => map[c]).ToArray();

		var A = new uint[N - 1];
		var B = new uint[N - 1];

		foreach (int i in Range(0, N - 1)) {
			A[i] = (3 + S[i + 1] - S[i]) % 3;
			B[i] = (3 + T[i] - T[i + 1]) % 3;
		}

		uint answer = 2;

		// ローリングハッシュ法で比較
		// オーバーフロー無視による暗黙の mod 2 ** 64

		unchecked {
			ulong hash_A = 0, hash_B = 0, power = 1;

			// A の接頭辞と B の接尾辞が一致する数を数える
			foreach (int i in Range(0, N - 1)) {
				hash_A *= 3;
				hash_A += A[i];
				hash_B += B[N - 2 - i] * power;

				if (hash_A == hash_B) answer++;

				power *= 3;
			}
		}

		unchecked {
			ulong hash_A = 0, hash_B = 0, power = 1;

			// A の接尾辞と B の接頭辞が一致する数を数える
			foreach (int i in Range(0, N - 2)) {
				hash_A += A[N - 2 - i] * power;
				hash_B *= 3;
				hash_B += B[i];

				if (hash_A == hash_B) answer++;

				power *= 3;
			}
		}

		WriteLine(answer);
	}
}
