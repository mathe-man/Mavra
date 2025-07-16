using System.Numerics;

namespace MavraLib;

public static class MavraMath
{
	public static Vector2 ForceBetween(Body body, Body other, Universe universe)
	{
		// Masses
		float m1 = body.Mass;
		float m2 = other.Mass;
		float G = universe.GravitationalConstant;
		// Distances
		float d = Vector2.Distance(body.Position, other.Position);
		Vector2 D = DistanceVectorBetween(body.Position, other.Position);
		
		
		// Make values real (NaN make error in calculus)
		d = !float.IsRealNumber(d)   ? 0 : d;
		D.X = !float.IsRealNumber(D.X) ? 0 : D.X;
		D.Y = !float.IsRealNumber(D.Y) ? 0 : D.Y;
		
		// Calculate the force and the force vector
		float force = G * m1 * m2 / (float)Math.Pow(d, 2);
		Vector2 forceVec = new(
			force * (D.X / Math.Abs(d)),
			force * (D.Y / Math.Abs(d))
		);
		
		forceVec.X = !float.IsRealNumber(forceVec.X) ? 0 : forceVec.X;
		forceVec.Y = !float.IsRealNumber(forceVec.Y) ? 0 : forceVec.Y;
		
		return forceVec;
	}

	public static Vector2 AccelerationFromForce(Vector2 force, float mass)
		// Speed is Sqrt(Force / distance)
		=> new(
			force.X / mass,
			force.Y / mass
		);

	public static float DistanceBetween(Vector2 value1, Vector2 value2)
	{
		// Calculate the x and y distance
		double x = value2.X - value1.X;
		double y = value2.Y - value1.Y;
		
		// Get the hypotenuse
		double distance = Math.Sqrt(x * x + y * y);
		
		return (float)distance;
	}
	public static Vector2 DistanceVectorBetween(Vector2 value1, Vector2 value2)
		=> new Vector2(value2.X - value1.X, value2.Y - value1.Y);
}