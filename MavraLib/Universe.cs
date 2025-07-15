using System.Collections.Generic;

namespace MavraLib;

public class Universe
{
	public List<Body> Bodies { get; set; } = new List<Body>();
	public float GravitationalConstant;


	public Universe(float gravitationalConstant, List<Body>? bodies = null)
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
}