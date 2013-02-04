using System.Collections.Generic;

public interface IMap
{
	Vec2 GetSize ();

	List<ITile> GetAllTiles ();

	void Update ();
	
	void Dispose ();
	
	/// <summary>
	/// Sets the visible region. All parameters are in World coordinate.
	/// </summary>
	/// <param name='x'>
	/// X.
	/// </param>
	/// <param name='y'>
	/// Y.
	/// </param>
	/// <param name='w'>
	/// W.
	/// </param>
	/// <param name='h'>
	/// H.
	/// </param>
	void SetVisibleRegion (float x, float y, float w, float h);
}