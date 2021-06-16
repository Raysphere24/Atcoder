using static System.Console;

class Program
{
	static bool IsPalindrome(string s)
	{
		for (int i = 0, j = s.Length - 1; i < j; i++, j--) {
			if (s[i] != s[j])
				return false;
		}

		return true;
	}

	static void Main()
	{
		string s = ReadLine();

		if (s == "0") {
			WriteLine("Yes");
			return;
		}

		int i = s.Length;

		while (s[i - 1] == '0')
			i--;

		string ss = s[0..i];

		//WriteLine(ss);

		WriteLine(IsPalindrome(ss) ? "Yes" : "No");
	}
}
