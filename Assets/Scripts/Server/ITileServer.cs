using System;

public interface ITileServer
{
	ITileResult GetTileData (int x, int y);
}