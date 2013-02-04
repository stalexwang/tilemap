
public interface ITileFactory
{
	/// <summary>
	/// Creates the tile at a given position.
	/// </summary>
	/// <returns>
	/// The tile.
	/// </returns>
	/// <param name='point'>
	/// Position of the tile to be created.
	/// The tile is one meter long and one meter wide.
	/// </param>
	ITile CreateTile (Vec2 position);
}