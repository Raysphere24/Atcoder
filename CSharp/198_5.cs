using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

class Program
{
	class Vertex
	{
		public readonly int Id, Color; // Id is 1-based
		public readonly List<Vertex> Neighbors;

		public Vertex(int id, int color)
		{
			Id = id;
			Color = color;
			Neighbors = new List<Vertex>();
		}
	}

	class Multiset<T>
	{
		private readonly Dictionary<T, int> dic = new Dictionary<T, int>();

		public void Add(T item)
		{
			if (dic.TryGetValue(item, out int count))
				dic[item] = count + 1;
			else
				dic[item] = 1;
		}

		public void Remove(T item)
		{
			int count = dic[item];

			if (count > 1)
				dic[item] = count - 1;
			else
				dic.Remove(item);
		}

		public bool Contains(T item) => dic.ContainsKey(item);
	}

	static void Main()
	{
		int N = int.Parse(ReadLine());
		Vertex[] vertices = ReadLine().Split(' ')
			.Select((color, index) => new Vertex(index + 1, int.Parse(color)))
			.ToArray();

		foreach (int i in Range(0, N - 1)) {
			string[] s = ReadLine().Split(' ');

			Vertex a = vertices[int.Parse(s[0]) - 1];
			Vertex b = vertices[int.Parse(s[1]) - 1];

			a.Neighbors.Add(b);
			b.Neighbors.Add(a);
		}

		var answer = new List<int>();

		var stack = new Stack<(Vertex, int)>();
		var path = new Stack<Vertex>();
		var colorsInPath = new Multiset<int>();

		stack.Push((vertices[0], 0));

		while (stack.Count > 0) {
			var (vertex, depth) = stack.Pop();

			while (path.Count > depth)
				colorsInPath.Remove(path.Pop().Color);

			//WriteLine(new string(' ', path.Count) + vertex.Id);

			if (!colorsInPath.Contains(vertex.Color))
				answer.Add(vertex.Id);

			foreach (Vertex neighbor in vertex.Neighbors) {
				if (path.Count == 0 || neighbor != path.Peek())
					stack.Push((neighbor, depth + 1));
			}

			path.Push(vertex);
			colorsInPath.Add(vertex.Color);
		}

		answer.Sort();

		foreach (int id in answer)
			WriteLine(id);
	}
}
