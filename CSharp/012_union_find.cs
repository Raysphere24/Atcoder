#nullable enable

//#define USE_STACK
//#define USE_RECURSION

using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

public class Node
{
	private Node? parent;

	// GetRoot() 関数は以下の3つの実装のどれでも動きます

#if USE_STACK // スタックを使用

	private static readonly Stack<Node> stack = new Stack<Node>();

	public Node GetRoot()
	{
		Node node = this;
		while (node.parent != null) {
			stack.Push(node);
			node = node.parent;
		}

		Node root = node;
		while (stack.Count > 0) {
			node = stack.Pop();
			node.parent = root;
		}

		return root;
	}

#elif USE_RECURSION // 再帰を使用

	public Node GetRoot()
	{
		if (parent == null) return this;

		return parent = parent.GetRoot();
	}

#else // どちらも使わない

	public Node GetRoot()
	{
		Node node = this;
		while (node.parent != null) {
			node = node.parent;
		}

		Node root = node;

		node = this;
		while (node.parent != null) {
			Node old_parent = node.parent;
			node.parent = root;
			node = old_parent;
		}

		return root;
	}

#endif

	public static void Merge(ref Node? node_1, ref Node? node_2)
	{
		if (node_1 == null) return;

		if (node_2 == null) {
			node_2 = node_1;
			return;
		}

		node_1 = node_1.GetRoot();
		node_2 = node_2.GetRoot();

		if (node_1 != node_2) {
			node_2.parent = node_1;
			node_2 = node_1;
		}
	}
}

public class Program
{
	public static void Main()
	{
		string[] s = ReadLine()!.Split(' ');
		int X = int.Parse(s[0]);
		int Y = int.Parse(s[1]);

		var M = new Node?[X + 2, Y + 2];

		int Q = int.Parse(ReadLine()!);

		foreach (int _ in Range(0, Q)) {
			s = ReadLine()!.Split(' ');

			if (s[0] == "1") {
				// add new point
				int x = int.Parse(s[1]);
				int y = int.Parse(s[2]);

				ref Node? n = ref M[x, y];

				Node.Merge(ref M[x - 1, y], ref n);
				Node.Merge(ref M[x + 1, y], ref n);
				Node.Merge(ref M[x, y - 1], ref n);
				Node.Merge(ref M[x, y + 1], ref n);

				n ??= new Node();
			}
			else {
				// query
				int x_1 = int.Parse(s[1]);
				int y_1 = int.Parse(s[2]);
				int x_2 = int.Parse(s[3]);
				int y_2 = int.Parse(s[4]);

				Node? a = M[x_1, y_1];
				Node? b = M[x_2, y_2];

				bool answer = a != null && b != null && a.GetRoot() == b.GetRoot();

				WriteLine(answer ? "Yes" : "No");
			}
		}
	}
}
