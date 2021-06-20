#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
//using static System.Math;
using static System.Linq.Enumerable;

using static Scanner;
using static Utils;

static class Scanner
{
	private static string[]? s;
	private static int i;

	public static string NextToken()
	{
		if (s == null || i == s.Length) {
			s = ReadLine()!.Split();
			i = 0;
		}

		return s[i++];
	}

	public static int NextInt() => int.Parse(NextToken());
	public static long NextLong() => long.Parse(NextToken());
	public static double NextDouble() => double.Parse(NextToken());
}

static class Utils
{
	public static T[] ReadArray<T>(Func<string, T> selector)
		=> ReadLine()!.Split().Select(selector).ToArray();

	public static T[] MakeArray<T>(int length, Func<int, T> selector)
		=> Range(0, length).Select(selector).ToArray();
}

class Vertex
{
	public readonly List<Vertex> Neighbors = new List<Vertex>();
	public int Index;
	public int InDegree;
	public int TempInDegree;
	public bool Visited;

	public Vertex(int index) { Index = index; }

	public override string ToString() => (Index + 1).ToString();
}

public class Program
{
	public static void Main()
	{
		int N = NextInt();
		int M = NextInt();
		int K = NextInt();

		Vertex[] vertices = MakeArray(N, i => new Vertex(i));

		foreach (int _ in Range(0, M)) {
			int a = NextInt() - 1;
			int b = NextInt() - 1;

			vertices[a].Neighbors.Add(vertices[b]);
			vertices[b].InDegree++;
		}

		var L = new List<Vertex>();
		var S = new List<Vertex>();

		// S から頂点を削除するときに選択すべき index を表す
		// 予め決められていない場合は S の末尾から消す
		var eraseIndices = new List<int>();

		var output = new StringBuilder();

		foreach (int iteration in Range(1, K)) {
			if (iteration > 1) {
				// 末尾の 0 をすべて消したあと末尾から 1 を引く
				// 例: {0, 1, 2, 0, 0} -> {0, 1, 1}
				while (eraseIndices[^1] == 0)
					eraseIndices.RemoveAt(eraseIndices.Count - 1);
				eraseIndices[^1]--;
			}

			L.Clear();
			S.Clear();
			S.AddRange(vertices.Where(v => v.InDegree == 0));

			foreach (Vertex v in vertices) {
				v.TempInDegree = v.InDegree;
				v.Visited = false;
			}

			// トポロジカルソート (Kahn's algorithm)
			while (S.Any()) {
				if (eraseIndices.Count == L.Count)
					eraseIndices.Add(S.Count - 1);

				int eraseIndex = eraseIndices[L.Count];
				Vertex v = S[eraseIndex];
				S.RemoveAt(eraseIndex);

				if (v.Visited) continue;
				v.Visited = true;
				L.Add(v);

				foreach (Vertex n in v.Neighbors) {
					if (--n.TempInDegree == 0 && !n.Visited) S.Add(n);
				}
			}

			if (vertices.Any(v => v.TempInDegree > 0) || iteration < K && eraseIndices.All(x => x == 0)) {
				WriteLine(-1);
				return;
			}

			output.AppendLine(string.Join(' ', L));
		}

		Write(output);
	}
}
