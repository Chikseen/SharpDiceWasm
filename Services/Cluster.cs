using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Cluster
{
	private List<ClusterData> _data = new();

	public Cluster(List<Vector2> data)
	{
		foreach (var pos in data)
		{
			_data.Add(new(pos));
		}

		GetNearestCluster();

		List<int> numberPerDice = new();
		for (int i = 1; i <= 6; i++)
		{
			var dice = _data.Where(c => c.Cluster == i).Count();
			numberPerDice.Add(dice);
		}
		State.Dices = numberPerDice;
	}

	private void GetNearestCluster()
	{
		if (_data.Count == 0)
			return;

		int clusterIndex = 1;

		for (int i = 0; i < _data.Count; i++)
		{
			for (int j = 0; j < _data.Count; j++)
			{
				if (i != j)
				{
					float distance = Vector2.Distance(_data[i].Position, _data[j].Position);

					if (_data[i].Cluster == -1)
					{
						_data[i].Cluster = clusterIndex;
						clusterIndex++;
					}

					if (distance < 35)
					{
						_data[j].Cluster = _data[i].Cluster;
					}
				}
			}
		}
	}
}