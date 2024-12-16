using System.Diagnostics;
using System.Linq;

var input = File.ReadAllLines("input.txt");
var sum = 0;
var sw = Stopwatch.StartNew();
var divisor = Array.IndexOf(input, "");

var rulesInput = input[..divisor];
var updateInput = input[(divisor + 1)..];

var rules = rulesInput
    .Select(input =>
    {
        var split = input.Split("|");
        return (before: int.Parse(split[0]), after: int.Parse(split[1]));
    })
    .ToArray();

var updates = updateInput
    .Select(input => input.Split(',').Select(page => int.Parse(page)).ToArray());

sum = updates
    .Where(update => !IsValid(update))
    .Select(Order)
    .Select(update => update[update.Length/2])
    .Sum();

sw.Stop();
Console.WriteLine($"{sum} in {sw.Elapsed} ms");

bool IsValid(int[] update)
{
    foreach (var rule in rules)
    {
        var beforeIndex = Array.IndexOf(update, rule.before);
        var afterIndex = Array.IndexOf(update, rule.after);

        if (beforeIndex < 0 || afterIndex < 0)
            continue;
        
        if (beforeIndex > afterIndex)
            return false;
    }

    return true;
}

int[] Order(int[] update)
{
    Array.Sort(update, (x, y) =>
    {
        if (rules.Any(rule => x == rule.before && y == rule.after))
            return -1;
        if (rules.Any(rule => x == rule.after && y == rule.before))
            return 1;
        return 0;
    });
    return update;
}