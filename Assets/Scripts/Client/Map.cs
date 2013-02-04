using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : IMap
{
	private Vec2 size;
	private GameObject root;
	private TileView prefab;
	private ITileFactory factory;
	private ITileLoadingStrategy loadingStrategy;
	private Dictionary<Vec2, TileView> tileSet;
	/// <summary>
	/// The visible tiles inside viewport.
	/// </summary>
	private List<ITile> visibleTiles;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Map"/> class.
	/// </summary>
	/// <param name='tileFactory'>
	/// Tile factory.
	/// </param>
	/// <param name='m'>
	/// M is the length of 1st dimension of the map.
	/// </param>
	/// <param name='n'>
	/// N is the length of 2nd dimemsion of the map.
	/// </param>
	public Map (List<ITile> tiles, ITileFactory tileFactory, ITileLoadingStrategy loadingStrategy, TileView prefab, int m, int n)
	{
		this.prefab = prefab;
		this.factory = tileFactory;
		this.loadingStrategy = loadingStrategy;
		this.size = new Vec2 (m, n);
		this.root = new GameObject ("Map");
		this.tileSet = new Dictionary<Vec2, TileView> (m * n);
		this.visibleTiles = new List<ITile> (m * n);
		
		LoadCachedTiles (tiles);
	}
	
	public void Update ()
	{
		foreach (var t in tileSet.Values) {
			t.GetTile ().Update ();
		}
	}
	
	public Vec2 GetSize ()
	{
		return size;
	}
	
	public List<ITile> GetAllTiles ()
	{
		List<ITile> tiles = new List<ITile> (tileSet.Count);
		foreach (var t in tileSet.Values) {
			tiles.Add (t.GetTile ());
		}
		return tiles;
	}
	
	public List<ITile> GetVisibleTiles ()
	{
		return visibleTiles;
	}
	
	public void SetVisibleRegion (float x, float y, float w, float h)
	{
		visibleTiles.Clear ();
		
		//normalize the parameters.
		float left = x / MapUtil.TILE_LENGTH;
		float lower = y / MapUtil.TILE_LENGTH;
		float right = w / MapUtil.TILE_LENGTH;
		float upper = h / MapUtil.TILE_LENGTH;
		
		//get all visible tiles inside the rigion
		ICollection<Vec2> points = loadingStrategy.GetTilesToLoad (left, lower, right, upper, size.x, size.y);
		
		foreach (var point in points) {
			//create the tile if not exist
			if (!tileSet.ContainsKey (point)) {
				ITile tile = factory.CreateTile (point);
				TileView renderer = CreateTile (point);
				renderer.SetTile (tile);
				tileSet.Add (point, renderer);
				visibleTiles.Add (tile);
			} else {
				visibleTiles.Add (tileSet [point].GetTile ());
			}
		}
	}
	
	public void Dispose ()
	{
		GameObject.Destroy (root);
		tileSet.Clear ();
	}
	
	private void LoadCachedTiles (List<ITile> tiles)
	{
		foreach (var tile in tiles) {
			Vec2 point = tile.GetPoint ();
			TileView renderer = CreateTile (point);
			renderer.SetTile (tile);
			tileSet.Add (point, renderer);
		}
	}
	
	private TileView CreateTile (Vec2 point)
	{
		TileView renderer = GameObject.Instantiate (prefab) as TileView;
		renderer.transform.parent = root.transform;
		
		return renderer;
	}
}