using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Eager loading strategy loads not only visible tiles but also ones that are likely to be visible.
/// It's based on LazyLoadingStrategy, and loads additional tiles according to users' actions.
/// </summary>
public class EagerLoadingStrategy : ITileLoadingStrategy
{
	/// <summary>
	/// The extra row or column tiles to load.
	/// </summary>
	private int extra = 3;
	private Vector2 lastPivot;
	private LazyLoadingStrategy lazyLoader;
	
	public EagerLoadingStrategy ()
	{
		lastPivot = Vector2.zero;
		lazyLoader = new LazyLoadingStrategy ();
	}
	
	public ICollection<Vec2> GetTilesToLoad (float x, float y, float w, float h, int m, int n)
	{
		ICollection<Vec2> visibleTiles = lazyLoader.GetTilesToLoad (x, y, w, h, m, n);
		List<Vec2> tiles = new List<Vec2> (visibleTiles);
		
		Rect rect = new Rect (x, y, w - x, h - y);
		Vector2 newPivot = new Vector2 (x, y);
		Vector2 delta = newPivot - lastPivot;
		Vector2 normal = delta.normalized;
		
		lastPivot = newPivot;
		
		foreach (var point in MapUtil.GetBorderPoints (x, y, w, h)) {
			Vector2 checkPoint = new Vector2 (point.x + delta.x, point.y + delta.y);
			if (!rect.Contains (checkPoint)) {
				foreach (var potentialPoint in GetPotentialTiles (point, normal)) {
					if (potentialPoint.x >= 0 && potentialPoint.x < m &&
						potentialPoint.y >= 0 && potentialPoint.y < n){
						tiles.Add (potentialPoint);
					}
				}
			}
		}
		
		return tiles;
	}
	
	private ICollection<Vec2> GetPotentialTiles (Vec2 point, Vector2 normal)
	{
		List<Vec2> tiles = new List<Vec2> ();
		
		for (int i = 1; i <= extra; i++) {
			Vector2 newPoint = new Vector2 (point.x, point.y) + normal * i;
			tiles.Add (new Vec2 (MapUtil.GetLeftBorder (newPoint.x), MapUtil.GetLowerBorder (newPoint.y)));
		}
		
		return tiles;
	}
}
