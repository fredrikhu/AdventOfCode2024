using System.Diagnostics;
using System.Linq;

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();

var size = (x: input[0].Length, y: input.Length);
var startPosition = input.Index()
    .Select(line => (x: line.Item.IndexOfAny(['^', 'v', '<', '>']), y: line.Index))
    .Single(line => line.x >= 0);

(int x, int y) startDirection = input[startPosition.y][startPosition.x] switch
{
    '^' => (0, -1),
    'v' => (0, 1),
    '<' => (-1, 0),
    '>' => (1, 0),
    _ => throw new ArgumentOutOfRangeException()
};

var count = 0;

var permutations = input.Index()
    .SelectMany(line => Enumerable.Range(0, line.Item.Length).Select(@char => (x: @char, y: line.Index)))
    .ToArray();


foreach (var permutation in permutations)
{
    var visitedLocations = new Dictionary<(int x, int y), HashSet<(int x, int y)>>();
    var position = startPosition;
    var direction = startDirection;
    
    if (input[permutation.y][permutation.x] == '#')
        continue;
    
    input[permutation.y] = string.Create(size.x, input[permutation.y], (span, c) =>
    {
        foreach (var @char in c.Index())
            span[@char.Index] = @char.Item;
        span[permutation.x] = '#';
    });

    while (true)
    {
        var nextPosition = (x: position.x + direction.x, y: position.y + direction.y);

        if (!InArea(nextPosition))
            break;

        if (!visitedLocations.ContainsKey(position))
            visitedLocations.Add(position, []);

        if (visitedLocations[position].Contains(direction))
        {
            count++;
            break;
        }
        
        visitedLocations[position].Add(direction);

        if (input[nextPosition.y][nextPosition.x] != '#')
        {
            position = nextPosition;
            continue;
        }

        direction = direction switch
        {
            (0, -1) => (1, 0),
            (0, 1) => (-1, 0),
            (-1, 0) => (0, -1),
            (1, 0) => (0, 1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
     
    input[permutation.y] = string.Create(size.x, input[permutation.y], (span, c) =>
    {
        foreach (var @char in c.Index())
            span[@char.Index] = @char.Item;
        span[permutation.x] = '.';
    });
}

sw.Stop();

Console.WriteLine($"{count} in {sw.Elapsed} ms");

bool InArea((int x, int y) checkedPosition)
{
    return checkedPosition is { x: >= 0, y: >= 0 } &&
           checkedPosition.x < size.x &&
           checkedPosition.y < size.y;
}
