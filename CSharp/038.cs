using System;

using static System.Console;

public static class Program
{
	static ulong gcd(ulong a, ulong b) => b == 0 ? a : gcd(b, a % b);

	public static void Main()
	{
		string[] s = ReadLine().Split();
		ulong A = ulong.Parse(s[0]);
		ulong B = ulong.Parse(s[1]);

		try {
			ulong GCD = gcd(A, B);
			ulong LCM = checked(A / GCD * B);

			WriteLine(LCM <= (ulong)1e18 ? LCM.ToString() : "Large");
		}
		catch (OverflowException) {
			WriteLine("Large");
		}
	}
}
