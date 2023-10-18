using System.Numerics;

public class ClusterData
{
	public Vector2 Position = new();
	public int Cluster = -1;

	public ClusterData(Vector2 pos)
	{
		Position = pos;
	}
}