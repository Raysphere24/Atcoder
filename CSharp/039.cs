#nullable enable

using System;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

class Vertex
{
	public List<Vertex> neighbors = new List<Vertex>();

	/*
	深さ優先探索によって部分木 (this) に対して問題の解を求める関数
	sumDistance: 全頂点対での距離の和
	sumDistanceToRoot: 全頂点に対するルートまでの距離の和
	descendants: 頂点数 (ルートを含む)
	*/
	public void Search(Vertex? parent, out ulong sumDistance, out ulong sumDistanceToRoot, out ulong descendants)
	{
		// ルートのみの1点からなる状態から始めて、部分木を1つずつマージしていく
		sumDistance = 0;
		sumDistanceToRoot = 0;
		descendants = 1;

		foreach (Vertex child in neighbors) {
			if (child == parent) continue;

			child.Search(this, out ulong childSumDistance, out ulong childSumDistanceToRoot, out ulong childDescendants);

			ulong childSumDistanceToNewRoot = childSumDistanceToRoot + childDescendants;

			sumDistance += childSumDistance
				+ sumDistanceToRoot * childDescendants
				+ childSumDistanceToNewRoot * descendants;

			descendants += childDescendants;
			sumDistanceToRoot += childSumDistanceToNewRoot;
		}
	}
}

public static class Program
{
	public static void Main()
	{
		int N = int.Parse(ReadLine()!);

		var vertices = Range(0, N).Select(i => new Vertex()).ToList();

		foreach (int _ in Range(0, N - 1)) {
			string[] s = ReadLine()!.Split();
			int a = int.Parse(s[0]) - 1;
			int b = int.Parse(s[1]) - 1;

			vertices[a].neighbors.Add(vertices[b]);
			vertices[b].neighbors.Add(vertices[a]);
		}

		Vertex root = vertices[0];
		root.Search(null, out ulong answer, out _, out _);

		WriteLine(answer);
	}
}
