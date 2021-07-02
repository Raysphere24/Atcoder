#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
using static System.Linq.Enumerable;

public class Vertex
{
	public readonly List<Vertex> OutgoingNeighbors = new List<Vertex>();
	public readonly List<Vertex> IncomingNeighbors = new List<Vertex>();

	public bool IsVisited = false;
	public Vertex? Component = null;

	public void Visit(Stack<Vertex> stack)
	{
		if (IsVisited) return;
		IsVisited = true;

		foreach (Vertex v in OutgoingNeighbors) {
			v.Visit(stack);
		}

		stack.Push(this);
	}

	public void Assign(Vertex root)
	{
		if (Component != null) return;
		Component = root;

		foreach (Vertex v in IncomingNeighbors) {
			v.Assign(root);
		}
	}
}

public class Program
{
	public static void Main()
	{
		string[] s = ReadLine()!.Split();
		int N = int.Parse(s[0]);
		int M = int.Parse(s[1]);

		Vertex[] vertices = Range(0, N).Select(i => new Vertex()).ToArray();

		foreach (var _ in Range(0, M)) {
			s = ReadLine()!.Split();
			int a = int.Parse(s[0]) - 1;
			int b = int.Parse(s[1]) - 1;

			vertices[a].OutgoingNeighbors.Add(vertices[b]);
			vertices[b].IncomingNeighbors.Add(vertices[a]);
		}

		var stack = new Stack<Vertex>(N);

		foreach (Vertex v in vertices)
			v.Visit(stack);

		// Stack の foreach は逆順
		foreach (Vertex v in stack)
			v.Assign(v);

		var components = new Dictionary<Vertex, long>();
		foreach (Vertex v in vertices) {
			Vertex c = v.Component!;
			components[c] = components.GetValueOrDefault(c) + 1;
		}

		WriteLine(components.Values.Sum(k => k * (k - 1) / 2));
	}
}
