// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

const string filePath = @"C:\Users\Administrator\elf_input.txt";
var input = File.ReadLinesAsync(filePath);
var matchPattern = @"mul\(\d{1,3},\d{1,3}\)";
var blockPattern = @"(do\(\)|don't\(\))";
var answer = 0;

var validRange = new List<(int start, int end)>();
bool isWithinDoBlock = true;
int blockStart = 0;

await foreach (var line in input)
{
    var matches = Regex.Matches(line, blockPattern);
    foreach (var blockMatch in matches)
    {
        var group = blockMatch as Group;
        if (blockMatch.ToString().Equals("do()"))
        {
            isWithinDoBlock = true;
            //blockStart = line.IndexOf(blockMatch.ToString());
            blockStart = group.Index + group.Length;
        }
        else if (blockMatch.ToString().Equals("don't()") && isWithinDoBlock)
        {
            validRange.Add((blockStart, group.Index));
            isWithinDoBlock = false;
        }
    }
}

foreach (var range in validRange)
{
    await foreach (var line in input)
    {
        var subString = line.Substring(range.start, range.end - range.start);
        var matches = Regex.Matches(subString, matchPattern);
        if(matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
                answer += MultiplyMatches(match.Value);
            }
        }
    }
}
Console.WriteLine($"Answer: {answer}");
//await SolutionA(input);

return;

int MultiplyMatches(string match)
{
    var splitMatch = match.Split(',');
    splitMatch[0] = splitMatch[0].Trim("mul(".ToCharArray());
    splitMatch[1] = splitMatch[1].Trim(")".ToCharArray());
    int.TryParse(splitMatch[0], out var firstNumber);
    int.TryParse(splitMatch[1], out var secondNumber);
    return firstNumber * secondNumber;
}

async Task SolutionA(IAsyncEnumerable<string> input)
{
    await foreach (var line in input)
    {
        var matches = Regex.Matches(line, matchPattern);
        if(matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
                answer += MultiplyMatches(match.Value);
            }
        }
    }
    Console.WriteLine($"Answer: {answer}");
}