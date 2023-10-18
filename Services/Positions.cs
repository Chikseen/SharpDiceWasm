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
}