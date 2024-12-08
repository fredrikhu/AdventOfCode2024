using System.Diagnostics;

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();
var size = (x: input[0].Length, y: input[1].Length);
int count = 0;

foreach (var line in input.Index())
{
    foreach (var c in line.Item.Index())
    {
        count += Xmas((x: c.Index, y: line.Index));
    }
}

sw.Stop();
Console.WriteLine($"{count} in {sw.Elapsed} ms");

int Xmas((int x, int y) startPosition)
{
    return MatchCount(startPosition, (x: 1, y: 0)) + 
           MatchCount(startPosition, (x: 1, y: 1)) +
           MatchCount(startPosition, (x: 0, y: 1)) +
           MatchCount(startPosition, (x: -1, y: 0)) +
           MatchCount(startPosition, (x: -1, y: -1)) +
           MatchCount(startPosition, (x: 0, y: -1)) +
           MatchCount(startPosition, (x: 1, y: -1)) +
           MatchCount(startPosition, (x: -1, y: 1));
}

int MatchCount((int x, int y) startPosition, (int x, int y) direction)
{
    var (x, y) = startPosition;
    foreach (var character in "XMAS")
    {
        if (x < 0 || y < 0 || x >= size.x || y >= size.y)
            return 0;
        if (input[x][y] != character)
            return 0;
        
        (x, y) = (x + direction.x, y + direction.y);
    }

    return 1;
}
