/// <summary>
/// A tile is a cell in the map.
/// </summary>
public interface ITile
{
	void Update ();
	
	void Subscribe (IObserver ob);

	int GetData ();
	
	void SetData (int data);

	Vec2 GetPoint ();
}