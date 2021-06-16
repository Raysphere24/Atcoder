#include <iostream>
#include <vector>
#include <cmath>

using uint = unsigned int;

class Factorizer
{
	std::vector<uint> primes;

public:
	Factorizer(uint max)
	{
		primes.push_back(2);

		for (uint i = 3; i <= max; i += 2) {
			bool is_prime = true;
			for (uint prime : primes) {
				if (prime * prime > i) {
					break;
				}
				if (i % prime == 0) {
					is_prime = false;
					break;
				}
			}
			if (is_prime) {
				primes.push_back(i);
			}
		}
	}

	std::vector<uint> factorize(uint n)
	{
		std::vector<uint> result;

		for (uint p : primes) {
			uint power = 0;
			while (n % p == 0) {
				power++;
				n /= p;
			}
			result.push_back(power);
		}

		return result;
	}

	uint gcd(std::vector<uint>& f_x, std::vector<uint>& f_y)
	{
		uint result = 1;

		uint i = 0;
		for (uint p : primes) {
			uint n = std::min(f_x[i], f_y[i]);
			result *= (uint)pow(p, n);
			i++;
		}

		return result;
	}
};

int main()
{
	uint A, B;
	std::cin >> A >> B;

	Factorizer factorizer(B);

	std::vector<std::vector<uint>> factors;

	for (uint n = A; n <= B; n++) {
		factors.push_back(factorizer.factorize(n));
	}

	uint N = B - A;

	uint result = 0;
	for (uint i = 0; i <= N; i++) {
		for (uint j = 0; j < i; j++) {
			result = std::max(result, factorizer.gcd(factors[i], factors[j]));
		}
	}

	std::cout << result << std::endl;
}
