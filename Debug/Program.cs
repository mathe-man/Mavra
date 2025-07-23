using System.Numerics;
using MavraLib;


class Program
{
	private static Body a = new(5E6f, new(0, 0))
		{ Name = "A Body" };
		
	private static Body b = new (500E6f, new (5, 1), 1, new (0, 0.07f))
		{ Name = "B Body" };
	
	
	private static Universe universe = new(6.67408E-11f, [a, b]);
	private static EvolutiveUniverse evo = new(universe);
	
	static void Main(string[] args)
	{
		DebugStateGeneration(args);
	}

	static void DebugStateGeneration(string[]? args = null)
	{
		Console.WriteLine();
		Console.WriteLine(universe.CalculateNextState(1));
	}

	static void DebugEvolution(string[]? args = null)
	{
		evo.ComputeEvolution(10000000, 1);
		
		evo.EnterConsoleInspection();
	}
}