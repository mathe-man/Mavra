namespace MavraLib;

public class MemoryEfficientEvolutiveUniverse
{
	public Universe Seed { get; private set; }
	public int SamplingInterval { get; private set; }
	
	public Dictionary<int, Universe> Samples { get; private set; }

	public MemoryEfficientEvolutiveUniverse(Universe seed, int samplingInterval = 250)
	{
		Seed = seed;
		SamplingInterval = samplingInterval;
		Samples = new Dictionary<int, Universe>() { [0] = seed };
	}
}