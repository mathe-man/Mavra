using System.Numerics;
using MavraLib;


class Program
{
	static void Main(string[] args)
	{
		Body b = new (14.7777f, Vector2.Zero);
		Universe u = new(4, [b]);
		Console.WriteLine(u);
	}
}