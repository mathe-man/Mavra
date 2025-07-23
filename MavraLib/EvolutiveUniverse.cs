namespace MavraLib;



public class EvolutiveUniverse
{
	public List<Universe> Evolution;
	public Universe Seed;

	public EvolutiveUniverse(Universe seed)
	{
		Seed = seed;
		Evolution = [Seed];
	}

	public void ComputeEvolution(int evolutionCount, float fixedDeltaTime = 0.1f)
	{
		for (int i = 0; i < evolutionCount; i++)
		{
			Evolution.Add(Evolution[i].CalculateNextState(fixedDeltaTime));
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