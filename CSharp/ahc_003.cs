#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

using static System.Console;
using static System.Math;
using static System.Linq.Enumerable;

public abstract class PriorityQueueNode
{
	// 優先順位 (小さい値が先に出てくる)
	public readonly double Priority;

	// キュー内の位置
	internal uint Index;

	public PriorityQueueNode(double priority) { Priority = priority; }
}

// イベントキュー (2分ヒープとして実装)
public class PriorityQueue<T> where T : PriorityQueueNode
{
	// 配列に格納 buffer[0] は使わずに buffer[1] から使う
	private T[] buffer;

	private uint capacity = 16; // 配列 buffer の容量，足りなくなったら指数的に拡張する
	private uint count = 0;     // キューに入っている要素数

	// コンストラクタ
	public PriorityQueue() { buffer = new T[capacity]; }

	// キューが空かどうかを取得するプロパティ
	public bool IsEmpty => count == 0;

	// キューに入っている要素数を取得するプロパティ
	public uint Count => count;

	// キューの先頭を取得する
	public T Top => count > 0 ? buffer[1]
		: throw new InvalidOperationException("The priority queue is empty");

	// キューを空にする
	public void Clear() { count = 0; }

	// キューにイベントを追加する
	public void Push(T item)
	{
		// count を 1つ増やし，容量オーバーしたら配列を拡張
		if (++count >= capacity) {
			capacity *= 2;
			var newBuffer = new T[capacity];
			buffer.CopyTo(newBuffer, 0);
			buffer = newBuffer;
		}

		// とりあえず item を末端に設定してから，適切な位置に移動する
		SetIndex(item, count);
		CascadeUp(item);
	}

	// Priority が最小のイベントをキューから取り出す
	public T Pop()
	{
		// キューの先頭を取得して削除して返す
		T top = Top;

		Delete(top);

		return top;
	}

	// キューからイベントを削除する
	public void Delete(T item)
	{
		if (buffer[item.Index] != item)
			throw new ArgumentException("The argument 'item' is invalid");

		// 末端の要素を取り出して，count を 1つ減らす
		T last = buffer[count--];

		// とりあえず last を item の位置に設定してから，適切な位置に移動する
		SetIndex(last, item.Index);

		CascadeUp(last);
		CascadeDown(last);
	}

	// location にあるイベントを item に置き換える
	public void Replace(T location, T item)
	{
		if (buffer[location.Index] != location)
			throw new ArgumentException("The argument 'location' is invalid");

		SetIndex(item, location.Index);

		CascadeUp(item);
		CascadeDown(item);
	}

	// item を index の位置に設定する
	private void SetIndex(T item, uint index)
	{
		item.Index = index;
		buffer[index] = item;
	}

	// item を入れるために適切な位置を item.index から上方向に探索して，そこに入れる
	private void CascadeUp(T item)
	{
		// item がルートに到達するまで
		while (item.Index > 1) {
			T parent = buffer[item.Index / 2];

			// ヒープ条件が満たされたら終了
			if (item.Priority > parent.Priority) return;

			// parent を 1つ下にずらす
			SetIndex(parent, item.Index);

			// item を 1つ上にずらす
			SetIndex(item, item.Index / 2);
		}
	}

	// item を入れるために適切な位置を item.index から下方向に探索して，そこに入れる
	private void CascadeDown(T item)
	{
		// 左右両方の子がいる間
		while (item.Index * 2 + 1 <= count) {
			T left = buffer[item.Index * 2];
			T right = buffer[left.Index + 1];

			// ヒープ条件が満たされたら終了
			if (item.Priority < left.Priority && item.Priority < right.Priority) return;

			if (left.Priority < right.Priority) {
				// left を 1つ上にずらす
				SetIndex(left, item.Index);

				// item を 1つ左下にずらす
				SetIndex(item, item.Index * 2);
			}
			else {
				// right を 1つ上にずらす
				SetIndex(right, item.Index);

				// item を 1つ右下にずらす
				SetIndex(item, item.Index * 2 + 1);
			}
		}

		// 左の子だけがいる場合
		if (item.Index * 2 == count) {
			T left = buffer[item.Index * 2];

			// ヒープ条件が満たされたら終了
			if (item.Priority < left.Priority) return;

			// left を 1つ上にずらす
			SetIndex(left, item.Index);

			// item を 1つ左下にずらす
			SetIndex(item, item.Index * 2);
		}
	}
}

public struct Distance
{
	public float Sum;
	public int Count;

	public static float Default = 1;

	public float Value => Count == 0 ? Default : Sum / Count;

	public void AddSample(float value) { Sum += value; Count++; }
}

public class SearchNode : PriorityQueueNode
{
	public readonly SearchNode? Parent;
	public readonly int R, C;
	public readonly char Movement;

	public SearchNode(double priority, SearchNode? parent, int r, int c, char movement)
		: base(priority) { Parent = parent; R = r; C = c; Movement = movement; }

	public string GetPath()
	{
		var s = new StringBuilder();

		SearchNode n = this;

		while (n.Parent != null) {
			s.Append(n.Movement);
			n = n.Parent;
		}

		return s.ToString();
	}
}

public class Program
{
	public static void Main()
	{
		var v = new Distance[29, 30];
		var h = new Distance[30, 29];

		static int MakeHash(int r, int c) => r * 30 + c;

		var closed = new HashSet<int>();

		var queue = new PriorityQueue<SearchNode>();

		float overallSum = 0;
		int overallCount = 0;

		foreach (int _ in Range(0, 1000)) {
			string? input = ReadLine();
			if (string.IsNullOrEmpty(input)) break;

			string[] s = input.Split(' ');

			int sr = int.Parse(s[0]);
			int sc = int.Parse(s[1]);
			int tr = int.Parse(s[2]);
			int tc = int.Parse(s[3]);

			queue.Clear();
			queue.Push(new SearchNode(0, null, tr, tc, ' '));

			closed.Clear();

			SearchNode n;

			while (true) {
				n = queue.Pop();

				if (n.R == sr && n.C == sc) break;

				if (!closed.Add(MakeHash(n.R, n.C))) continue;

				if (n.Movement != 'U' && n.R > 0 && !closed.Contains(MakeHash(n.R - 1, n.C)))
					queue.Push(new SearchNode(n.Priority + v[n.R - 1, n.C].Value, n, n.R - 1, n.C, 'D'));

				if (n.Movement != 'D' && n.R < 29 && !closed.Contains(MakeHash(n.R + 1, n.C)))
					queue.Push(new SearchNode(n.Priority + v[n.R, n.C].Value, n, n.R + 1, n.C, 'U'));

				if (n.Movement != 'L' && n.C > 0 && !closed.Contains(MakeHash(n.R, n.C - 1)))
					queue.Push(new SearchNode(n.Priority + h[n.R, n.C - 1].Value, n, n.R, n.C - 1, 'R'));

				if (n.Movement != 'R' && n.C < 29 && !closed.Contains(MakeHash(n.R, n.C + 1)))
					queue.Push(new SearchNode(n.Priority + h[n.R, n.C].Value, n, n.R, n.C + 1, 'L'));
			}

			string path = n.GetPath();

			WriteLine(path);

			float actualDistance = float.Parse(ReadLine()!);
			float averageEdgeDistance = actualDistance / path.Length;

			while (n.Parent != null) {
				switch (n.Movement) {
					case 'D': v[n.R, n.C].AddSample(averageEdgeDistance); break;
					case 'U': v[n.R - 1, n.C].AddSample(averageEdgeDistance); break;
					case 'R': h[n.R, n.C].AddSample(averageEdgeDistance); break;
					case 'L': h[n.R, n.C - 1].AddSample(averageEdgeDistance); break;
				}
				n = n.Parent;
			}

			//overallSum += averageEdgeDistance;
			//overallCount++;

			overallSum += actualDistance;
			overallCount += path.Length;

			Distance.Default = overallSum / overallCount;
		}
	}
}
