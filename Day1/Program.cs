// 2024 Advent of Code, Day 1, parts 1 & 2
// see: https://adventofcode.com/2024 for details

var inputFile = await File.ReadAllLinesAsync("/home/wchesley/Documents/elf_locations.txt");
var columnA = new List<int>();
var columnB = new List<int>();
foreach (var line in inputFile)
{
    var splitLine = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
    columnA.Add(int.Parse(splitLine[0]));
    columnB.Add(int.Parse(splitLine[1]));
}
columnA = columnA.OrderBy(x => x).ToList();
columnB = columnB.OrderBy(x => x).ToList();

var differenceList = new List<int>();
for (int i = 0; i < columnA.Count; i++)
{
    var diff = columnB[i] - columnA[i];
    differenceList.Add(Math.Abs(diff));
}
var differenceSum = differenceList.Sum();

Console.WriteLine($"Part 1: {differenceSum}");

var similarList = new Dictionary<int, int>();
foreach (var colAValue in columnA)
{
    foreach (var colBValue in columnB)
    {
        if (colAValue == colBValue)
        {
            if(similarList.TryGetValue(colAValue, out var value))
            {
                similarList[colAValue] = value + 1;
            }
            else
            {
                similarList.Add(colAValue, 1);
            }
        }
    }
}

//multiply each dictionary key by its value and sum the results
var similarSum = similarList.Sum(x => x.Key * x.Value);
Console.WriteLine($"Part 2: {similarSum}");