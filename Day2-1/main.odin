package main

import "core:fmt"
import "core:math"
import "core:os"
import "core:strconv"
import "core:strings"

My_Int :: distinct int

main :: proc() {
	data, ok := os.read_entire_file("input.txt")
	if !ok {
		fmt.print("Could not read file")
		return
	}
	defer delete(data, context.allocator)

	count: i32 = 0
	it := string(data)
	line_loop: for line in strings.split_lines_iterator(&it) {
		numbers_as_string := strings.split_multi(line, []string{" "})
		numbers := make([]int, len(numbers_as_string))
		defer delete(numbers, context.allocator)
		for number_as_string, index in numbers_as_string {
			numbers[index] = strconv.atoi(number_as_string)
		}

		up := numbers[1] > numbers[0]
		for i in 1 ..< len(numbers) {
			diff := math.abs(numbers[i] - numbers[i - 1])
			if up != (numbers[i] > numbers[i - 1]) || diff == 0 || diff > 3 {
				continue line_loop
			}
		}

		count += 1
	}

	fmt.printf("%d", count)
}
