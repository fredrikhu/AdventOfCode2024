using System.Diagnostics;

var input = File.ReadAllText("input.txt").AsSpan();
int index = 0;
int sum = 0;

var sw = Stopwatch.StartNew();
while (true)
{
	index = input.IndexOf("mul(");
	if (index < 0) break;
	input = input.Slice(index + 4);

	var length = 0;
	while (length < input.Length && char.IsDigit(input[length]))
		++length;

	if (length == 0 || length > 3)
		continue;

	var firstAsString = input.Slice(0, length);
	int first = int.Parse(firstAsString);

	input = input.Slice(length);
	if (input[0] != ',')
		continue;

	input = input.Slice(1);

	length = 0;
	while (length < input.Length && char.IsDigit(input[length]))
		++length;

	if (length == 0 || length > 3)
		continue;

	var secondAsString = input.Slice(0, length);
	int second = int.Parse(secondAsString);

	input = input.Slice(length);

	if (input[0] != ')')
		continue;

	sum += first * second;
}

Console.WriteLine(sum);
sw.Stop();
Console.WriteLine(sw.Elapsed);