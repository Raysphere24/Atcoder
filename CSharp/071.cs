#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
//using static System.Math;
using static System.Linq.Enumerable;

using static Scanner;

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

class Vertex
{
	public readonly List<Vertex> Neighbors = new List<Vertex>();
	public readonly int Name;
	public int InDegree;
	public int TempInDegree;

	public Vertex(int name) { Name = name; }
}

public class Program
{
	public static void Main()
	{
		int N = NextInt();
		int M = NextInt();
		int K = NextInt();

		Vertex[] vertices = Range(1, N).Select(i => new Vertex(i)).ToArray();

		foreach (int _ in Range(1, M)) {
			int a = NextInt() - 1;
			int b = NextInt() - 1;

			vertices[a].Neighbors.Add(vertices[b]);
			vertices[b].InDegree++;
		}

		var L = new List<int>();
		var S = new List<Vertex>();

		// S から頂点を削除するときに選択すべき index を表す
		// 予め決められていない場合は S の末尾から消す
		var eraseIndices = new List<int>();

		var output = new StringBuilder();

		foreach (int iteration in Range(1, K)) {
			if (iteration > 1) {
				// eraseIndices の末尾の 0 をすべて消したあと末尾から 1 を引く
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
			}

			// トポロジカルソート (Kahn's algorithm)
			while (S.Any()) {
				if (eraseIndices.Count == L.Count)
					eraseIndices.Add(S.Count - 1);

				int eraseIndex = eraseIndices[L.Count];
				Vertex v = S[eraseIndex];
				S.RemoveAt(eraseIndex);

				L.Add(v.Name);

				foreach (Vertex n in v.Neighbors) {
					if (--n.TempInDegree == 0) S.Add(n);
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
