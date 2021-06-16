#include <iostream>
#include <cmath>

int main()
{
	double X, Y, Z;
	std::cin >> X >> Y >> Z;

	std::cout << ceil(Y * Z / X) - 1 << std::endl;
}
