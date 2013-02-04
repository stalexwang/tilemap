using System.Collections.Generic;

[System.Serializable]
public class Tile : ITile
{
	/// <summary>
	/// The position of the tile.
	/// </summary>
	private Vec2 point;
	
	/// <summary>
	/// The content of the tile.
	/// </summary>
	private int data;
	
	[System.NonSerialized]
	private List<IObserver> observers;
	
	public Tile (Vec2 point, int data)
	{
		this.point = point;
		this.data = data;
		this.observers = new List<IObserver> ();
	}
	
	public void SetData (int data)
	{
		this.data = data;
		
		//notify all observers
		foreach (var o in observers) {
			o.Notify ();
		}
	}
	
	public int GetData ()
	{
		return data;
	}
	
	public Vec2 GetPoint ()
	{
		return point;
	}
	
	public void Update ()
	{
	}
	
	public void Subscribe (IObserver ob)
	{
		observers.Add (ob);
	}
	
	public override string ToString ()
	{
		return string.Format ("[Tile: ({0}, {1}) = {2}]", point.x, point.y, data);
	}
}