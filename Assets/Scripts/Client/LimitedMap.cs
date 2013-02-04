using System;
using System.Collections.Generic;
using UnityEngine;

public class LimitedMap : IMap
{
	private Map map;
	private int maxTileCount;
	//record the tiles' life time
	private Dictionary<ITile, float> tileLifetime;
	
	public LimitedMap (Map map, int maxTileCount)
	{
		this.map = map;
		this.maxTileCount = maxTileCount;
		this.tileLifetime = new Dictionary<ITile, float> ();
		
		foreach (var t in map.GetAllTiles ()) {
			tileLifetime [t] = 0;
		}
	}
	
	public Vec2 GetSize ()
	{
		return map.GetSize ();
	}
	
	public List<ITile> GetAllTiles ()
	{
		List<ITile> allTiles = map.GetAllTiles ();
		List<ITile> tiles = new List<ITile> (maxTileCount);
		
		//sort the tiles by their life time
		allTiles.Sort ((ITile t1, ITile t2) => {
			return tileLifetime [t2].CompareTo (tileLifetime [t1]);});
		
		tiles.AddRange (allTiles.GetRange (0, maxTileCount));
		
		return tiles;
	}
	
	public void SetVisibleRegion (float x, float y, float w, float h)
	{
		map.SetVisibleRegion (x, y, w, h);
		
		foreach (var t in map.GetVisibleTiles ()) {
			if (!tileLifetime.ContainsKey (t)) {
				tileLifetime.Add (t, 0);
			}
		}
	}
	
	public void Update ()
	{
		map.Update ();
		
		foreach (var t in map.GetVisibleTiles ()) {
			tileLifetime [t] += Time.deltaTime;
		}
	}
	
	public void Dispose ()
	{
		tileLifetime.Clear ();
		map.Dispose ();
	}
}