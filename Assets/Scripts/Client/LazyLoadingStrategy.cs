using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lazy loading strategy only loads tiles that are visible.
/// </summary>
public class LazyLoadingStrategy : ITileLoadingStrategy
{
	public ICollection<Vec2> GetTilesToLoad (float x, float y, float w, float h, int m, int n)
	{
		List<Vec2> tiles = new List<Vec2> ();
		
		//clamp the position
		int left = MapUtil.GetLeftBorder (Mathf.Clamp (x, 0, w));
		int lower = MapUtil.GetLowerBorder (Mathf.Clamp (y, 0, h));
		int right = MapUtil.GetRightBorder (Mathf.Clamp (w, x, m));
		int upper = MapUtil.GetUpperBorder (Mathf.Clamp (h, y, n));
		
		for (int i = left; i < right; i++) {
			for (int j = lower; j < upper; j++) {
				tiles.Add (new Vec2 (i, j));
			}
		}
		
		return tiles;
	}
}
