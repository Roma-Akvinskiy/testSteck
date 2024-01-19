using System;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Hello World");

		List<string> arr = new List<string> { "A", "B", "C", "D", "S", "W", "A", "B", "B", "C", "G" };
		List<string> inputs = new List<string> { "C", "B" };

		Search sortThis = new Search(arr, inputs);

		Console.WriteLine(sortThis.search(["C"]));
		Console.WriteLine(sortThis.search(["B"]));
	}
}


public class Search
{
	List<string> arr;
	List<string> inputs;

	public Search(List<string> arr, List<string> inputs)
	{
		arr = this.arr;
		inputs = this.inputs;
	}

	public Dictionary<string, int> search()
	{

		return null;
	}
}

