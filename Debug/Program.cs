using System.Numerics;
using MavraLib;


class Program
{
	private static Body a = new(500E6f, new(0, 0))
		{ Name = "A Body" };
		
	private static Body b = new (150E6f, new (5, 1), 1, new (0, 0.07f))
		{ Name = "B Body" };
	
	
	private static Universe universe = new(6.67408E-11f, [a, b]);
	private static EvolutiveUniverse evo = new(universe);
	
	static void Main(string[] args)
	{
		DebugEvolution(args);
	}

	static void DebugEvolution(string[]? args = null)
	{
		evo.ComputeEvolution(10000000);
		
		evo.EnterConsoleInspection();
	}
}