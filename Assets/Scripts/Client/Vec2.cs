/// <summary>
/// A 2D vector which represents the indices of a tile.
/// </summary>
[System.Serializable]
public struct Vec2
{
	public int x;
	public int y;
		
	public Vec2 (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}