<Query Kind="Program">
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

string[] rawInput = File.ReadAllLines(MyExtensions.GetFilePathFromQueryDir("day12.txt"));

void Main()
{
	var graph = ConstructGraph(rawInput);
	
	graph.Dump();
	
}

// You can define other methods, fields, classes and namespaces here

public class Node
{
	public Node(string name)
	{
		Name = name;
		Size = name.ToLower() == name ? Size.Small : Size.Big;
	}
	
	public string Name {get;set;}
	public Size Size {get;set;}
	
	public List<string> Connections {get;} = new List<string>();
}

public enum Size
{
	Big,
	Small
}

public IDictionary<string, Node> ConstructGraph(string[] connections)
{
	ConcurrentDictionary<string, Node> nodes = new ConcurrentDictionary<string, Node>();
	
	foreach (var connection in connections)
	{
		var conns = connection.Split('-').Dump();
		var first = conns[0];
		var second = conns[1];
		
		var n = nodes.GetOrAdd(first, new Node(first));
		n.Connections.Add(second);
		
		n = nodes.GetOrAdd(first, new Node(second));
		n.Connections.Add(first);
	}
	
	return nodes;
}

