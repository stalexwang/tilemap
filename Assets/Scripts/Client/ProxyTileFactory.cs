using System;
using UnityEngine;

public class ProxyTileFactory : ITileFactory
{
	private ITileServer server;
	
	public ProxyTileFactory (ITileServer server)
	{
		this.server = server;
	}
	
	public ITile CreateTile (Vec2 point)
	{		
		return new ProxyTile (server, point);;
	}
}