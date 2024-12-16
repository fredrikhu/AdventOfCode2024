using System.Diagnostics;
using System.Linq;

var input = File.ReadAllLines("input.txt");
var equations = input
    .Select(line => line.Split(':'))
    .Select(line => new Equation
    {
        Result = long.Parse(line[0]),
        Operands = line[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray()
    });

long result = 0;

var sw = Stopwatch.StartNew();

foreach (var equation in equations)
{
    if (Solveable(equation))
    {
        result += equation.Result;
    }
}

sw.Stop();

Console.WriteLine($"{result} in {sw.Elapsed} ms");


bool Solveable(Equation o)
{
    var results = new List<long> { o.Operands.First() };

    foreach (var op in o.Operands.Skip(1))
    {
        results = results
            .SelectMany(r => (long[])[r + op, r * op, long.Parse($"{r}{op}")])
            .ToList();
    }
    
    return results.Any(r => r == o.Result);
}

class Equation
{
    public long Result { get; set; }
    public long[] Operands { get; set; }
}