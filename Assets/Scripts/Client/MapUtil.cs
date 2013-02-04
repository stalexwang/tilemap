using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Map utilities.
/// </summary>
public class MapUtil
{
	/// <summary>
	/// length of the tile gameObject.
	/// </summary>
	public const float TILE_LENGTH = 10f;
	
	public static int GetLeftBorder (float x)
	{
		return Mathf.FloorToInt (x);
	}
	
	public static int GetLowerBorder (float y)
	{
		return Mathf.FloorToInt (y);
	}
	
	public static int GetRightBorder (float w)
	{
		return Mathf.CeilToInt (w);
	}
	
	public static int GetUpperBorder (float h)
	{
		return Mathf.CeilToInt (h);
	}
	
	public static ICollection<Vec2> GetBorderPoints (float x, float y, float w, float h)
	{
		List<Vec2> points = new List<Vec2> ();
		
		int left = GetLeftBorder (x);
		int right = GetRightBorder (w);
		int lower = GetLowerBorder (y);
		int upper = GetUpperBorder (h);
		
		for (int i = left; i <= right; i++) {
			points.Add (new Vec2 (i, lower));
			points.Add (new Vec2 (i, upper));
		}
		
		for (int j = lower - 1; j < upper; j++) {
			points.Add (new Vec2 (left, j));
			points.Add (new Vec2 (right, j));
		}
		
		return points;
	}
}