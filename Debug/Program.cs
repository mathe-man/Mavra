using System.Numerics;
using MavraLib;


class Program
{
	static void Main(string[] args)
	{
		Body a = new (500E6f, new (0, 0));
		a.Name = "A";
		Body b = new (150E6f, new (5, 1), 1, new (0, 0.07f));
		b.Name = "B";
		
		Universe seed = new(6.67408E-11f, [a, b]);
		EvolutiveUniverse evo = new(seed);
		evo.ComputeEvolution(10000000);
		
		evo.EnterConsoleInspection();
	}
}