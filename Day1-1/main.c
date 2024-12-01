#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <urlmon.h>

char *read_file(char const path[static 1]);

typedef struct {
  int left;
  int right;
} list_item;

list_item next_distance(char **string);
int count_lines(char const string[static 1]);
int compare_int(const void *x, const void *y) { return *(int *)x - *(int *)y; }

int main()
{
	int accumulated_distance = 0;
	char *file_content = read_file("input.txt");
	char *file_iterator = file_content;
	int lines = count_lines(file_content);

	int left[lines];
	int right[lines];

	int current_line = 0;
	while (*file_iterator!= '\0')
	{
		list_item item = next_distance(&file_iterator);

		if (item.left == 0 && item.right == 0)
			break;

		left[current_line] = item.left;
		right[current_line] = item.right;

		current_line++;
	}

	qsort(left, lines, sizeof(int), compare_int);
	qsort(right, lines, sizeof(int), compare_int);

	current_line = 0;
	while (current_line < lines)
	{
		accumulated_distance += abs(left[current_line] - right[current_line]);
		current_line++;
	}
	printf("%d\n", accumulated_distance);
	return 0;
}

list_item next_distance(char **string)
{
	char *before = *string;
	int first = strtol(*string, string, 10);
	int second = strtol(*string, string, 10);

	if (before == *string)
		return (list_item){ 0 };

	return (list_item){
		.left = first,
		.right = second
	};
}

int count_lines(char const string[static 1])
{
	int lines = 0;
	while (*string != '\0')
	{
		if (*string == '\n')
			lines++;

		string++;
	}

	return lines;
}

char *read_file(char const path[static 1])
{
	FILE *file = NULL;
	if (fopen_s(&file, path, "rb") != 0) return NULL;

	if (fseek(file, 0, SEEK_END) != 0) return NULL;
	const size_t length = ftell(file);
	if (fseek(file, 0, SEEK_SET) != 0) return NULL;

	size_t read_bytes = 0;
	char *buffer = malloc(length + 1);
	if (buffer)
	{
		buffer[length] = 0;
		read_bytes = fread(buffer, 1, length, file);

		if (read_bytes != length)
		{
			free(buffer);
			return NULL;
		}
	}

	fclose(file);

	return buffer;
}
