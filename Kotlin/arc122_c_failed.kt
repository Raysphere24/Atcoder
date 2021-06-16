//import java.util.Scanner
import kotlin.math.*

const val sqrt5 = 2.23606797749979
const val phi = (sqrt5 + 1) / 2

fun main() {
//	val input = Scanner(System.`in`)
//	var x = (phi.pow(32) / sqrt5).roundToLong()
//	val z = 1000000000000000000L
	val z = readLine()!!.toLong()

	val n = log(z.toDouble(), phi).toInt()
	val a = z / phi.pow(n)

	val operations = ArrayList<Int>()

	var x = 0L
	var y = 0L
	var i = 0

	if (n % 2 == 1) {
		y = 1
		operations.add(2)
	}
	else {
		x = 1
		operations.add(1)
	}

//	i++
//	println("$x, $y")

	while (x < z) {
		if (x > y) {
			val y2 = (a * phi.pow(i)).roundToLong()
//			println(y2)
			if (y < y2) {
				operations.add(2)
				y++
			}
			operations.add(4)
			y += x
		}
		else {
			val x2 = (a * phi.pow(i)).roundToLong()
//			println(x2)
			if (x < x2) {
				operations.add(1)
				x++
			}
			operations.add(3)
			x += y
		}
//		println("$x, $y")
		i++
	}

//	var y = (x.toDouble() / phi).toLong()
//	var y = (a * phi.pow(n - 1)).toLong()

//	println("$x, $y")
//
//	val operations = ArrayList<Int>()
//
//	while (x > 0 && y > 0) {
//		if (x > y) {
//			val x2 = x - y
////			val x2 = (y * phi).toLong()
////			println(x2)
//			if (x2 < x) {
//				operations.add(1)
//				x--
//			}
//			else {
//				operations.add(3)
//				x -= y
//			}
////			operations.add(3)
////			x -= y
//		}
//		else {
//			val y2 = (x * phi).roundToLong()
//			println(y2)
//			if (y2 < y) {
//				operations.add(2)
//				y--
//			}
//			else {
//				operations.add(4)
//				y -= x
//			}
////			operations.add(4)
////			y -= x
//		}
//		println("$x, $y")
//	}
//
//	while (y > 0) {
//		operations.add(2)
//		y--
//	}
//
//	while (x > 0) {
//		operations.add(1)
//		x--
//	}

//	operations.reverse()

	println(operations.size)
	println(operations.joinToString(separator = "\n"))
}
