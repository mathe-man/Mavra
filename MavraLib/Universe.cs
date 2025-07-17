using System.Collections.Generic;
using System.Numerics;

namespace MavraLib;

public class Universe
{
	public List<Body> Bodies { get; set; } = new List<Body>();
	public float GravitationalConstant;


	public Universe(float gravitationalConstant = 6.67408E-11f, List<Body>? bodies = null)
	{
		GravitationalConstant = gravitationalConstant;

		if (bodies != null)
			Bodies = bodies;
	}
	
	public Universe CalculateNextState()
	{
		var next = new Universe(GravitationalConstant);
		foreach (var b in Bodies)
			next.Bodies.Add(b.CalculateNextState(this));
		
		return next;
	}

	public override string ToString()
	{
		string result = $"Universe with {Bodies.Count} body.";
		if (Bodies.Count == 0)
			return result + " {}";

		// Open curly bracket
		result += "\n{\n";
		
		// Add each body with indentation
		foreach (var body in Bodies)
			result += "\t" + body + "\n";

		// Close curly bracket
		result += "}";
		
		return result;
	}

	public string Visual()
	{
		(int min, int max) xAxis = (-5, 5);
		(int min, int max) yAxis = (-5, 5);
		
		foreach (var body in Bodies)
		{
			xAxis.min = Math.Min((int)body.Position.X, xAxis.min);
			xAxis.max = Math.Max((int)body.Position.X, xAxis.max);
			
			yAxis.min = Math.Min((int)body.Position.Y, yAxis.min);
			yAxis.max = Math.Max((int)body.Position.Y, yAxis.max);
		}


		string result = "";
		for (int y = yAxis.max; y >= yAxis.min; y--)
		{
			for (int x = xAxis.min; x <= xAxis.max; x++)
			{
				char here = ' ';
				if (x == 0 && y == 0)
					here = '0';
			
				foreach (var b in Bodies)
					if (new Vector2((int)b.Position.X, (int)b.Position.Y) == new Vector2(x, y))
						here = b.Name[0];

				result += here;
			}

			result += '\n';
		}

		return result;
	}
}

public class EvolutiveUniverse
{
	public List<Universe> Evolution;
	public Universe Seed;

	public EvolutiveUniverse(Universe seed)
	{
		Seed = seed;
		Evolution = [Seed];
	}

	public void ComputeEvolution(int evolutionCount)
	{
		int progress_percent = 0;
		for (int i = 0; i < evolutionCount; i++)
		{
			Evolution.Add(Evolution[i].CalculateNextState());
			
			if (i % evolutionCount / 20 == 0) // Every 5% of the computation
			{
				progress_percent += 5;
				Console.WriteLine($"Evolution computing progress: {progress_percent}%");
			}
		}
	}


	public void EnterConsoleInspection()
	{
		ConsoleKeyInfo key = new();
		int inspection_index = 0;

		bool initialCursorState = Console.CursorVisible;
		Console.CursorVisible = false;
		
		while (key.Key != ConsoleKey.Escape)
		{
			Console.Clear();
			Console.WriteLine($"Evolution N°{inspection_index} of {Evolution.Count}");
			
			// Visualize the current evolution
			Console.WriteLine(Evolution[inspection_index].Visual());
			Console.WriteLine(Evolution[inspection_index]);

			// Inspector help
			Console.WriteLine("\nKeys:" +
			                  "\nEscape -> Escape the inspector" +
			                  "\nNavigations: " +
			                  "\nLeft Arrow   -> Inspect precedent evolution" +
			                  "\nRight Arrow  -> Inspect next evolution" +
			                  "\nCtrl + Left Arrow -> Go back 10 evolution before" +
			                  "\nCtrl + Right Arrow -> Go ahead 10 evolution after");

			key = Console.ReadKey(true);
			
			// Determine how much the index should be moved
			int index_move = 0;
			if (key.Key == ConsoleKey.LeftArrow)
				index_move = -1;
			else if (key.Key == ConsoleKey.RightArrow)
				index_move = 1;
			if (key.Modifiers.HasFlag(ConsoleModifiers.Control))
				index_move *= 10;
				
			// Check if the requested index is not out of range
			bool WillBeInRange = 
				inspection_index + index_move < Evolution.Count &&
				inspection_index + index_move >= 0;
			
			if (WillBeInRange)
				inspection_index += index_move;
		}
		
		Console.CursorVisible = initialCursorState;
	}
}