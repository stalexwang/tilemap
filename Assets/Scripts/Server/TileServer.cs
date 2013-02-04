using System;
using System.Threading;
using UnityEngine;

public class TileServer : ITileServer
{
	/// <summary>
	/// The tile set which is a 2d array.
	/// </summary>
	private int[,] tileSet;
	private const int MIN_TILE_DATA = 1;
	private int maxTileData;
	private int latency;
	private int interval;
	private System.Random random;
	private Thread updateProc;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="TileServer"/> class.
	/// </summary>
	/// <param name='m'>
	/// M is the length of 1st dimension.
	/// </param>
	/// <param name='n'>
	/// N is the lenght of 2nd dimension.
	/// </param>
	/// <param name="maxData">
	/// maxData is the maximum value of the data in a tile.
	/// </param>
	/// <param name = "interval"
	/// the lifetime of the tiles.
	/// </param>
	public TileServer (int m, int n, int maxData, int interval, int latency)
	{
		this.tileSet = new int[m, n];
		this.maxTileData = maxData;
		this.latency = latency;
		this.interval = interval;
		this.random = new System.Random ((int)DateTime.Now.Ticks);
		this.updateProc = new Thread (Update);
		
		updateProc.Start ();
	}
	
	public ITileResult GetTileData (int x, int y)
	{
		x = Mathf.Clamp (x, 0, tileSet.GetLength (0) - 1);
		y = Mathf.Clamp (y, 0, tileSet.GetLength (1) - 1);
		
		return new TileResult (tileSet [x, y], latency);
	}
	
	private void Update ()
	{
		for (;;) {
			GenerateRandomTileData ();
		
			Thread.Sleep (interval * 1000);
		}
	}
	
	private void GenerateRandomTileData ()
	{
		//fill the tile set with random number between min and max
		for (int i = 0; i < tileSet.GetLength (0); i++) {
			for (int j = 0; j < tileSet.GetLength (1); j++) {
				tileSet [i, j] = random.Next (MIN_TILE_DATA, maxTileData + 1);
			}
		}
	}
}