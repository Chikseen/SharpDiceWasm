using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Positions
{
	public List<Vector2> PositionList = new();

	public float GetDistanceToNearestPos(int x, int y)
	{
		float minDistance = float.MaxValue;
		foreach (Vector2 pos in PositionList)
		{
			float distance = Vector2.Distance(new Vector2(x, y), new Vector2(pos.X, pos.Y));
			if (distance < minDistance)
				minDistance = distance;
		}
		return minDistance;
	}

	public Vector2 GetNearestPos(List<Vector2> positionList, float x, float y)
	{
		float minDistance = float.MaxValue;
		Vector2 minPos = new();
		foreach (Vector2 pos in positionList)
		{
			float distance = Vector2.Distance(new Vector2(x, y), pos);
			if (distance < minDistance)
			{
				minDistance = distance;
				minPos = pos;
			}
		}
		return minPos;
	}

	public List<int> GetClusterAndNumber(Positions data)
	{
		List<int> response = new();
		List<Vector2> positionList = data.PositionList;

		while (positionList.Count > 0)
		{
			int eyeCount = 1;
			Vector2 position = positionList.First();
			positionList.RemoveAt(0);

			eyeCount += CountPossibleNeighbor(positionList, position);
			response.Add(eyeCount);
		}

		return response;
	}

	private int CountPossibleNeighbor(List<Vector2> positionList, Vector2 position)
	{
		Vector2 nearestPos = GetNearestPos(positionList, position.X, position.Y);
		float distance = Vector2.Distance(position, nearestPos);
		if (distance < 0)
		{
			positionList.Remove(nearestPos);
			Console.WriteLine("here");
			return 1 + CountPossibleNeighbor(positionList, nearestPos);
		}
		else
		{
			return 0;
		}
	}
}