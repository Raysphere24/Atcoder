import java.util.*

fun main(args: Array<String>) {
	val input = Scanner(System.`in`)
	val queue = PriorityQueue<Long>()
	var offset = 0L

	repeat(input.nextInt()) {
		when (input.nextInt()) {
			1 -> queue.add(input.nextLong() - offset)
			2 -> offset += input.nextLong()
			3 -> println(queue.poll() + offset)
		}
	}
}
