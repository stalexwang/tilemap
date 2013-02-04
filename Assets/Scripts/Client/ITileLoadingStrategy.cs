using System.Collections.Generic;
/// <summary>
/// The strategy to determine which tiles to load.
/// </summary>
public interface ITileLoadingStrategy
{
	/// <summary>
	/// Gets the tiles to load.
	/// The paramters x, y, w, h are all normalized,
	/// which means the tile is one meter long and one meter wide.
	/// </summary>
	/// <returns>
	/// The indices of the tiles.
	/// </returns>
	/// <param name='x'>
	/// x of the viewport.
	/// </param>
	/// <param name='y'>
	/// Y of the viewport.
	/// </param>
	/// <param name='w'>
	/// Width of the viewport.
	/// </param>
	/// <param name='h'>
	/// Height of the viewport.
	/// </param>
	/// <param name='m'>
	/// Width of the map.
	/// </param>
	/// <param name='n'>
	/// Height of the map.
	/// </param>
	ICollection<Vec2> GetTilesToLoad (float x, float y, float w, float h, int m, int n);
}