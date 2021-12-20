<Query Kind="Statements" />

var rawInput = File.ReadAllLines(MyExtensions.GetFilePathFromQueryDir("day6.txt"));

IEnumerable<IEnumerable<string>> ArraySplit(string[] input)
{
	var start = 0;
	var end = 0;
	
	for (int i = 0; i < input.Length; i++)
	{
		if (string.IsNullOrWhiteSpace(input[i]))
		{
			end = i;
			yield return input.AsSpan(start, end-start).ToArray();
			start = i+1;
		}
	}
	
	yield return input.AsSpan(start, input.Length-start).ToArray();
}

ArraySplit(rawInput)
.Select(i => string.Join("", i).ToCharArray().Distinct().Count())
.Sum()
.Dump();


ArraySplit(rawInput)
.Select(i => new 
	{
		countOfPeople = i.Count(),
		countOfInputs = string.Join("", i).ToCharArray()
			.GroupBy(s => s)                                // group by input
			.Select(s => new { input = s.Key, count = s.Count() })
			.Where(x => x.count == i.Count())
	})
.Sum(x => x.countOfInputs.Count())
.Dump();
