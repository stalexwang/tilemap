using UnityEngine;

public class TileView : MonoBehaviour, IObserver
{
	[SerializeField]
	private GameObject[] prefabs;//list of objects in a tile
	private GameObject tileObject;
	private int curIndex = -1;
	private ITile tile;
	
	public ITile GetTile ()
	{
		return tile;
	}
	
	public int GetPrefabCount ()
	{
		return prefabs.Length;
	}
	
	public void SetTile (ITile tile)
	{	
		this.tile = tile;
		
		tile.Subscribe (this);
		
		Vec2 point = tile.GetPoint ();
		transform.position = new Vector3 ((point.x + 0.5f) * MapUtil.TILE_LENGTH, 0, (point.y + 0.5f) * MapUtil.TILE_LENGTH);
		
		gameObject.name = string.Format ("Tile ({0}, {1})", point.x, point.y);
		
		UpdateTileObject ();
	}
	
	public void Notify ()
	{
		UpdateTileObject ();
	}
	
	private void UpdateTileObject ()
	{
		int index = tile.GetData ();
		
		if (index == curIndex)
			return;
		
		if (index >= 0 && index < prefabs.Length) {	
			GameObject.Destroy (tileObject);
			tileObject = GameObject.Instantiate (prefabs [index]) as GameObject;
			tileObject.transform.parent = transform;
			tileObject.transform.localPosition = Vector3.zero;
			curIndex = index;
		}
	}
}