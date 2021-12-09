<Query Kind="Statements" />

var rawInput = File.ReadAllLines(MyExtensions.GetFilePathFromQueryDir("day9.txt"));

var heightMap = rawInput
	.Select(line => 
		line.ToCharArray()
			.Select(x => int.Parse(x.ToString()))
			.ToArray())
	.ToArray();//.Dump();

var minima = 
	from x in Enumerable.Range(0, 100)
	from y in Enumerable.Range(0, 100)
	where IsMinima(heightMap, x,y)
	select (x,y);

minima.Select(p => heightMap[p.x][p.y] + 1).Sum().Dump("part1");

minima.Select(p => new { minima = p, size = GetSizeUsingFloodFill(heightMap, p.x, p.y)})
	.OrderByDescending(x => x.size)
	.Take(3)
	.Select(x => x.size)
	.Aggregate(1, (x, y) => x*y)
	.Dump("part2");

// for each minima, do a flood fill algorithm to find size
int GetSizeUsingFloodFill(int[][] heightMap, int x, int y)
{
	var nodesToCheck = new Queue<(int x, int y)>(GetNeighborAddresses(x, y));
	
	var validNodes = new HashSet<(int x, int y)>();
	var checkedNodes = new HashSet<(int x, int y)>();
	
	validNodes.Add((x,y)); // initial position is a valid node
	
	while (nodesToCheck.TryDequeue(out var point))
	{
		checkedNodes.Add(point);
		
		if (heightMap[point.x][point.y] == 9)
			continue;
			
		validNodes.Add(point);
		
		foreach (var newNode in GetNeighborAddresses(point.x, point.y))
		{
			if (checkedNodes.Contains(newNode))
				continue;
			nodesToCheck.Enqueue(newNode);
		}
	}
	
	return validNodes.Count;
}

IEnumerable<(int x, int y)> GetNeighborAddresses(int i, int j)
{
	if (i > 0)
		yield return (i-1, j);
	
	if (i < 99)
		yield return (i+1, j);

	if (j > 0)
		yield return (i, j-1);

	if (j < 99)
		yield return (i, j+1);
}

bool IsMinima(int[][] heightMap, int i, int j)
{
	return heightMap[i][j] < GetNeighborAddresses(i, j).Select(x => heightMap[x.x][x.y]).Min();
}

