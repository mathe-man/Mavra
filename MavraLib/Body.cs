using System.Numerics;

namespace MavraLib;

public class Body
{
	public float Mass       { get; private set; }
	public float Radius     { get; private set; }
	public Vector2 Position { get; private set; }
	public Vector2 Velocity { get; private set; }

	public string Name { get; set; } = "Body";
	// The RGBA color
	public Vector4 Color    { get; set; } = new Vector4(1, 1, 1, 1);	


	public Body(float mass, Vector2 position, float radius = 10f, Vector2? velocity = null)
	{
		this.Mass     = mass;
		this.Radius   = radius;
		this.Position = position;
		this.Velocity = velocity ?? new Vector2();
	}
	public Body CalculateNextState(Universe universe)
	{
		var next = this;
		next.Position += Vector2.One * universe.GravitationalConstant;
		return next;
	}

	public override string ToString() 
		=> $"{Name}:  Mass:{Mass:0.###} ; Radius:{Radius:0.###} | Position {Position:0.###} ; Velocity {Velocity:0.###}";
}