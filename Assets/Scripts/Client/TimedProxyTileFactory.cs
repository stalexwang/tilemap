
public class TimedProxyTileFactory : ITileFactory
{
	private float timeOut;
	private ITileServer server;
	
	public TimedProxyTileFactory (ITileServer server, float timeOut)
	{
		this.timeOut = timeOut;
		this.server = server;
	}
	
	public ITile CreateTile (Vec2 point)
	{
		ProxyTile proxyTile = new ProxyTile (server, point);
		TimedProxyTile tile = new TimedProxyTile (proxyTile, timeOut); 
		return tile;
	}
}