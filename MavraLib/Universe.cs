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
	
	public Universe CalculateNextState(float deltaTime)
	{
		var next = new Universe(GravitationalConstant);
		foreach (var b in Bodies)
			next.Bodies.Add(b.CalculateNextState(this, deltaTime));
		
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

