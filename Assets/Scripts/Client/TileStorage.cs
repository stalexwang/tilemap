using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TileStorage
{
	//used to create tiles when reading from file.
	private ITileFactory factory;
	
	public TileStorage (ITileFactory factory)
	{
		this.factory = factory;
	}
	
	public List<ITile> Load (string fileName)
	{
		List<ITile> tileSet = new List<ITile> ();
		
		if (File.Exists (fileName)) {
			FileStream stream = File.OpenRead (fileName);
			if (stream.CanRead) {
				BinaryFormatter formatter = new BinaryFormatter ();
				
				List<Tile> tiles = formatter.Deserialize (stream) as List<Tile>;
				Debug.Log ("Loaded tiles: " + tiles.Count);
				
				foreach (var t in tiles) {
					ITile tile = factory.CreateTile (t.GetPoint ());
					tile.SetData (t.GetData ());
					tileSet.Add (tile);
				}
			}
			
			stream.Close ();
		}
		
		return tileSet;
	}
	
	public void Save (string fileName, List<ITile> tileSet)
	{
		FileStream stream = File.Create (fileName);
		
		if (stream.CanWrite) {
			BinaryFormatter formatter = new BinaryFormatter ();
			Debug.Log ("Saved tiles: " + tileSet.Count);
			
			List<Tile> tiles = new List<Tile> (tileSet.Count);
			foreach (var t in tileSet) {
				tiles.Add (new Tile (t.GetPoint (), t.GetData ()));
			}
			formatter.Serialize (stream, tiles);
		}
		
		stream.Close ();
	}
}