package main

import "base:intrinsics"
import "core:fmt"
import "core:math"
import "core:os"
import "core:slice"
import "core:strconv"
import "core:strings"

My_Int :: distinct int

main :: proc() {
	data, ok := os.read_entire_file("input.txt")
	if !ok {
		fmt.print("Could not read file")
		return
	}
	defer delete(data)

	count: int = 0
	it := string(data)
	line_loop: for line in strings.split_lines_iterator(&it) {
		numbers_as_string := strings.split_multi(line, []string{" "})
		numbers := make([dynamic]int, len(numbers_as_string))
		defer delete(numbers)
		for number_as_string, index in numbers_as_string {
			numbers[index] = strconv.atoi(number_as_string)
		}
		dampener_used := false

		//diff := math.abs(numbers[0] - numbers[1])
		//if diff == 0 || diff > 3 {
		//	ordered_remove(&numbers, 0)
		//	dampener_used = true
		//}

		if safe_route(numbers, false, 1) {
			count += 1
		}
	}

	fmt.printf("%d", count)
}

safe_route :: proc(numbers: [dynamic]int, dampener_used: bool, i: int) -> bool {
	i := i
	numbers := numbers
	up := numbers[1] > numbers[0]
	for i < len(numbers) {
		diff := math.abs(numbers[i] - numbers[i - 1])
		if up != (numbers[i] > numbers[i - 1]) || diff == 0 || diff > 3 {
			if dampener_used {
				return false
			}
			if i + 1 == len(numbers) {
				return true
			}
			left := slice.clone_to_dynamic(numbers[:])
			defer delete(left)
			right := slice.clone_to_dynamic(numbers[:])
			defer delete(right)

			ordered_remove(&left, i - 1)
			ordered_remove(&right, i)
			fmt.println(numbers)
			fmt.println(left)
			fmt.println(right)
			return safe_route(left, true, 1) || safe_route(right, true, 1)
		}
		i += 1
	}
	return true
}
