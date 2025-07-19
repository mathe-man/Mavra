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
	private byte[] _color = [255, 255, 255, 255];
	public byte[] Color
	{
		get { return _color;}
		set
		{
			if (value.Length < 3)
				return;
			
			_color = value;

			if (_color.Length == 3)
				// Add the Alpha component
				_color = [_color[0], _color[1], _color[2], 255];
				
		}
	} 


	public Body(float mass, Vector2 position, float radius = 1f, Vector2? velocity = null)
	{
		this.Mass     = mass;
		this.Radius   = radius;
		this.Position = position;
		this.Velocity = velocity ?? new Vector2();
	}
	public Body CalculateNextState(Universe universe)
	{
		var next = new Body(Mass, Position, Radius, Velocity);
		next.Name = Name;
		
		Vector2 velocity = new();
		foreach (var body in universe.Bodies)
			if (this != body)
				if (MavraMath.DistanceBetween(Position, body.Position) > Radius + body.Radius)
					velocity += MavraMath.AccelerationFromForce(MavraMath.ForceBetween(this, body, universe), Mass);
					
		
		next.Position += this.Velocity;
		next.Velocity += velocity;
		
		return next;
	}

	public override string ToString() 
		=> $"{Name}:  Mass:{Mass:0.###} ; Radius:{Radius:0.###} | Position {Position} ; Velocity {Velocity}";
}