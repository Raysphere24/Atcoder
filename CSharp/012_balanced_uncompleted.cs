#nullable enable

using System.Collections.Generic;

using System.Diagnostics;

using static System.Console;
using static System.Linq.Enumerable;

public class Node
{
	public Node? Parent;
	public Node? Child1, Child2;

	private int depth;

	public void Append(Node node)
	{
		Debug.Assert(node.Child1 == null && node.Child2 == null);

		if (Child2 == null) {
			Child2 = node;
			node.Parent = Child2;
		}
		else {
			Child2.Append(node);
		}
	}

	private Node Pop()
	{
		Node result;

		if (Child1 == null) {
			Debug.Assert(depth == 0);

			if (this == Parent!.Child1)
				Parent.Child1 = null;
			else if (this == Parent.Child2)
				Parent.Child2 = null;
			else
				throw new System.Exception();

			Parent = null;
			result = this;
		}
		else if (Child2 == null || Child1.depth > Child2.depth) {
			Debug.Assert(Child2 == null || Child1.depth == Child2.depth + 1);
			result = Child1.Pop();
		}
		else {
			Debug.Assert(Child1.depth == Child2.depth);
			result = Child2.Pop();
		}

		Updatedepth();

		return result;
	}

	private void Updatedepth()
	{
		if (Child2 != null)
			depth = System.Math.Max(Child1!.depth, Child2.depth);
		else if (Child1 != null)
			depth = Child1.depth;
		else
			depth = 0;
	}

	private void Balance()
	{
		Debug.Assert(Child1 == null || Child2 == null || Child1.depth >= Child2.depth);

		if (Child1 == null) {
			Debug.Assert(Child2 == null);
			return;
		}

		if (Child2 == null) {
			if (Child1.depth <= 1)
				return;
			// Rotate...
		}
		else {
			if (Child1.depth <= Child2.depth + 1)
				return;
			// Rotate...

			//Debug.Assert()
			// Child1 and Child2 are already balanced

			//Node a = Child1, b = Child2;
			//Node a_1 = ;
		}
	}

	public static void Merge(Node a, Node b, Node root)
	{
		if (a.depth < b.depth) {
			Merge(b, a, root);
			return;
		}

		root.Child1 = a;
		root.Child2 = b;

		a.Parent = root;
		b.Parent = root;

		root.Balance();
	}

	public static void Merge(Node a, Node b)
	{
		if (a.depth < b.depth) {
			Merge(b, a, b.Pop());
		}
		else if (a.depth == b.depth) {
			Merge(a, b, b.Pop());
		}
		else {
			Merge(a, b, a.Pop());
		}
	}

	public void AddTo(HashSet<Node> nodes) => nodes.Add(this);

	public Node Root => Parent == null ? this : Parent.Root;
}

public class Program
{
	static (int, int) ReadIntPair()
	{
		var s = ReadLine()!.Split(' ');
		return (int.Parse(s[0]), int.Parse(s[1]));
	}

	public static void Main()
	{
		var (H, W) = ReadIntPair();

		var M = new Node?[H, W];

		int Q = int.Parse(ReadLine()!);

		var nodesToMerge = new HashSet<Node>();

		foreach (int _ in Range(0, Q)) {
			string[] s = ReadLine()!.Split(' ');

			if (s[0] == "1") {
				int r = int.Parse(s[1]);
				int c = int.Parse(s[2]);

				var node = new Node();

				M[r - 1, c]?.AddTo(nodesToMerge);
				M[r + 1, c]?.AddTo(nodesToMerge);
				M[r, c - 1]?.AddTo(nodesToMerge);
				M[r, c + 1]?.AddTo(nodesToMerge);

				M[r, c] = node;
			}
			else {
				int ra = int.Parse(s[1]);
				int ca = int.Parse(s[2]);
				int rb = int.Parse(s[3]);
				int cb = int.Parse(s[4]);

				Node? a = M[ra, ca];
				Node? b = M[rb, cb];

				bool answer = (a != null && b != null && a.Root == b.Root);

				WriteLine(answer ? "Yes" : "No");
			}
		}
	}
}
