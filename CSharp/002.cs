using static System.Console;

class Program
{
	class SearchState
	{
		private readonly char[] chars;
		private readonly int total_length;
		private int current_position, current_depth;

		public SearchState(int n) { 
			chars = new char[n];
			total_length = n;
		}

		public void Search()
		{
			if (current_position == total_length) {
				WriteLine(chars);
				return;
			}

			if (current_position + current_depth + 2 <= total_length) {
				chars[current_position] = '(';
				current_position++;
				current_depth++;

				Search();

				current_position--;
				current_depth--;
			}

			if (current_depth > 0) {
				chars[current_position] = ')';
				current_position++;
				current_depth--;

				Search();

				current_position--;
				current_depth++;
			}
		}
	}

	static void Main()
	{
		int N = int.Parse(ReadLine());

		if ((N & 1) == 1) return;

		var state = new SearchState(N);

		state.Search();
	}
}
