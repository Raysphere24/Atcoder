#nullable enable

//using System;
using System.Collections.Generic;

using static System.Console;
using static System.Linq.Enumerable;

using Connection = System.Collections.Generic.List<Vertex>;

class Vertex
{
	public readonly List<Connection> Connections = new List<Connection>();
	public int Distance = -1;
}

public class Program
{
	public static void Main()
	{
		var s = ReadLine()!.Split();
		int N = int.Parse(s[0]);
		int M = int.Parse(s[1]);

		var vertices = Range(0, N).Select(i => new Vertex()).ToList();

		foreach (int i in Range(0, M)) {
			ReadLine();

			Connection connection = ReadLine()!.Split()
				.Select(s => vertices[int.Parse(s) - 1]).ToList();

			foreach (Vertex v in connection)
				v.Connections.Add(connection);
		}

		var queue = new Queue<Vertex>();
		vertices[0].Distance = 0;
		queue.Enqueue(vertices[0]);

		var visited = new HashSet<Connection>();

		while (queue.Any()) {
			var vertex = queue.Dequeue();

			int newDistance = vertex.Distance + 1;

			foreach (var connection in vertex.Connections) {
				if (!visited.Add(connection)) continue;

				foreach (var neighbor in connection) {
					if (neighbor.Distance >= 0) continue;

					neighbor.Distance = newDistance;
					queue.Enqueue(neighbor);
				}
			}
		}

		foreach (var vertex in vertices) {
			WriteLine(vertex.Distance);
		}
	}
}
