using System.Diagnostics;

var input = File.ReadAllText("input.txt").AsSpan();
var sum = 0;

var sw = Stopwatch.StartNew();
while (true)
{
	var index = input.IndexOf("mul(");
	if (index < 0) break;
	input = input[(index + 4)..];

	var length = 0;
	while (length < input.Length && char.IsDigit(input[length]))
		++length;

	if (length is 0 or > 3)
		continue;

	var firstAsString = input.Slice(0, length);
	int first = int.Parse(firstAsString);

	input = input[length..];
	if (input[0] != ',')
		continue;

	input = input[1..];

	length = 0;
	while (length < input.Length && char.IsDigit(input[length]))
		++length;

	if (length is 0 or > 3)
		continue;

	var secondAsString = input.Slice(0, length);
	int second = int.Parse(secondAsString);

	input = input[length..];

	if (input[0] != ')')
		continue;

	sum += first * second;
}

Console.WriteLine(sum);
sw.Stop();
Console.WriteLine(sw.Elapsed);