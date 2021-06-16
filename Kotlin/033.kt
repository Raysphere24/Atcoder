fun main()
{
	val (H, W) = readLine()!!.split(' ').map(String::toInt)

	print(if (H == 1 || W == 1) (H * W) else ((H + 1) / 2) * ((W + 1) / 2))
}
