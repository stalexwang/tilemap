using UnityEngine;

/// <summary>
/// A lazy tile is only loaded whe the player sees it.
/// </summary>
public class ProxyTile : ITile
{
	private bool loaded;
	private Tile tile;
	private Vec2 point;
	private ITileServer server;
	private ITileResult result;
	
	public ProxyTile (ITileServer server, Vec2 point)
	{
		this.loaded = false;
		this.point = point;
		this.server = server;
		this.tile = new Tile (point, 0);
	}
	
	/// <summary>
	/// Refresh the tile data.
	/// </summary>
	public void Refresh ()
	{
		result = server.GetTileData (point.x, point.y);
	}
	
	public int GetData ()
	{
		if (!loaded) {
			loaded = true;
			
			Refresh ();
		}
		
		return tile.GetData ();
	}
	
	public void SetData (int data)
	{
		tile.SetData (data);
	}
	
	public Vec2 GetPoint ()
	{
		return point;
	}
	
	public void Update ()
	{
		if (result != null && result.IsDone) {
			tile.SetData (result.Data);
			result = null;
		}
		
		tile.Update ();
	}
	
	public void Subscribe (IObserver ob)
	{
		tile.Subscribe (ob);
	}
}