using System.Diagnostics;

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();
var size = (x: input[0].Length, y: input[1].Length);
int count = 0;

for (int y = 1; y < size.y - 1; y++)
{
    for (int x = 1; x < size.x - 1; x++)
    {
        if (Xmas((x, y)))
            ++count;
    }
}

sw.Stop();
Console.WriteLine($"{count} in {sw.Elapsed} ms");

bool Xmas((int x, int y) startPosition)
{
    return Matches((startPosition.x - 1, startPosition.y - 1), (x: 1, y: 1)) && 
           (
               Matches((startPosition.x - 1, startPosition.y + 1), (x: 1, y: -1)) ||
               Matches((startPosition.x + 1, startPosition.y - 1), (x: -1, y: 1))
           ) ||
           Matches((startPosition.x + 1, startPosition.y + 1), (x: -1, y: -1)) && 
           (
               Matches((startPosition.x - 1, startPosition.y + 1), (x: 1, y: -1)) ||
               Matches((startPosition.x + 1, startPosition.y - 1), (x: -1, y: 1))
           );
}

bool Matches((int x, int y) startPosition, (int x, int y) direction)
{
    var (x, y) = startPosition;
    foreach (var character in "MAS")
    {
        if (x < 0 || y < 0 || x >= size.x || y >= size.y)
            return false;
        if (input[x][y] != character)
            return false;
        
        (x, y) = (x + direction.x, y + direction.y);
    }

    return true;
}
