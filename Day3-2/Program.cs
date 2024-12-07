using System.Diagnostics;

var input = File.ReadAllText("input.txt").AsSpan();
var sum = 0;
bool enabled = true;

var sw = Stopwatch.StartNew();
while (true)
{
	var mulIndex = input.IndexOf("mul(");
	if (mulIndex < 0) break;

	var dontIndex = input.IndexOf("don't()");
	if (dontIndex < mulIndex && dontIndex >= 0)
		enabled = false;
	
	var doIndex = input.IndexOf("do()");
	if (doIndex < mulIndex && doIndex >= 0)
		enabled = true;
	
	input = input[(mulIndex + 4)..];

	if (!enabled)
		continue;
	
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