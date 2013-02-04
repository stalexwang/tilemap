using System;

public class AppConfig
{
	/// <summary>
	/// The size of the map.
	/// </summary>
	public Vec2 MapSize = new Vec2 (100, 100);
	/// <summary>
	/// The name of the map file.
	/// </summary>
	public string MapFileName = "Assets/storage";
	/// <summary>
	/// The user drag speed.
	/// </summary>
	public float UserDragSpeed = 1;
	/// <summary>
	/// Enable the refreshing of the tiles.
	/// </summary>
	public bool RefreshTiles = false;
	/// <summary>
	/// The refreshing interval in seconds.
	/// </summary>
	public int RefreshIntervalSeconds = 5;
	/// <summary>
	/// Control the file size of the map storage.
	/// </summary>
	public bool LimitTilesCount = false;
	/// <summary>
	/// The max number of tiles to save.
	/// </summary>
	public int MaxTilesCount = 20;
	/// <summary>
	/// Load potentially visible tiles.
	/// </summary>
	public bool LoadPotentialTiles = false;
	/// <summary>
	/// The columns of potential tiles to load.
	/// </summary>
	public int PotentialTileColumns = 3;
}