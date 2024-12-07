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

		fmt.eprintln(numbers)
		if is_safe(numbers, false) {
			count += 1
		}
	}

	fmt.printf("%d", count)
}

is_safe :: proc(numbers: [dynamic]int, dampener_used: bool) -> bool {
	numbers := numbers

	for number, index in numbers {
		i := index + 1
		up := numbers[1] - numbers[0] > 0
		if i == len(numbers) {
			return true
		}

		if numbers[i] == numbers[i - 1] {
			if dampener_used {
				return false
			}

			modified_numbers := slice.clone_to_dynamic(numbers[:])
			defer free(&modified_numbers)
			ordered_remove(&modified_numbers, i - 1)
			fmt.eprintln(modified_numbers)
			return is_safe(modified_numbers, true)
		}

		if abs(numbers[i] - numbers[i - 1]) > 3 || (numbers[i] - numbers[i - 1] > 0) != up {
			if dampener_used {
				return false
			}

			modified_numbers := slice.clone_to_dynamic(numbers[:])
			defer free(&modified_numbers)
			ordered_remove(&modified_numbers, i)
			fmt.eprintln(modified_numbers)
			if is_safe(modified_numbers, true) {
				return true
			}

			modified_numbers = slice.clone_to_dynamic(numbers[:])
			defer free(&modified_numbers)
			ordered_remove(&modified_numbers, i - 1)
			fmt.eprintln(modified_numbers)
			if is_safe(modified_numbers, true) {
				return true
			}

			if i < 2 {
				return false
			}

			modified_numbers = slice.clone_to_dynamic(numbers[:])
			defer free(&modified_numbers)
			ordered_remove(&modified_numbers, i - 2)
			fmt.eprintln(modified_numbers)
			return is_safe(modified_numbers, true)
		}
	}

	return true
}
