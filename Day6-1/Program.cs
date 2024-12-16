using System.Diagnostics;
using System.Linq;

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();

var size = (x: input[0].Length, y: input.Length);
var position = input.Index()
    .Select(line => (x: line.Item.IndexOfAny(['^', 'v', '<', '>']), y: line.Index))
    .Single(line => line.x >= 0);

(int x, int y) direction = input[position.y][position.x] switch
{
    '^' => (0, -1),
    'v' => (0, 1),
    '<' => (-1, 0),
    '>' => (1, 0),
    _ => throw new ArgumentOutOfRangeException()
};

var distinctPositions = 0;

while (true)
{
    // PrintBoard();

    var nextPosition = (x: position.x + direction.x, y: position.y + direction.y);

    if (!InArea(nextPosition))
    {
        input[position.y] = string.Create(size.x, input[position.y], (span, c) =>
        {
            foreach (var @char in c.Index())
                span[@char.Index] = @char.Item;
            span[position.x] = 'X';
        });
        
        // PrintBoard();
        
        ++distinctPositions;
        break;
    }

    if (input[nextPosition.y][nextPosition.x] != '#')
    {
        if (input[position.y][position.x] != 'X')
        {
            input[position.y] = string.Create(size.x, input[position.y], (span, c) =>
            {
                foreach (var @char in c.Index())
                    span[@char.Index] = @char.Item;
                span[position.x] = 'X';
            });
            
            ++distinctPositions;
        }

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

sw.Stop();

Console.WriteLine($"{distinctPositions} in {sw.Elapsed} ms");

bool InArea((int x, int y) checkedPosition)
{
    return checkedPosition is { x: >= 0, y: >= 0 } &&
           checkedPosition.x < size.x &&
           checkedPosition.y < size.y;
}

// void PrintBoard()
// {
//     foreach (var line in input)
//         Console.WriteLine(line);
//     Console.WriteLine();
// }
