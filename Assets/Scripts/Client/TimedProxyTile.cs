using UnityEngine;

public class TimedProxyTile : ITile
{
	private float timeOut;
	private float timer;
	private bool active;
	private bool visible;
	private ProxyTile tile;
	
	public TimedProxyTile (ProxyTile tile, float timeOut)
	{
		this.timer = 0;
		this.active = false;
		this.visible = false;
		this.tile = tile;
		this.timeOut = timeOut;
	}
	
	public void Update ()
	{
		if (active && visible) {
			timer += Time.deltaTime;
			
			//refresh tile when time out
			if (timer > timeOut) {
				timer = 0;
				tile.Refresh ();
			}
		}
		
		tile.Update ();
	}
	
	public void Subscribe (IObserver ob)
	{
		tile.Subscribe (ob);
	}
	
	public int GetData ()
	{
		active = true;
		visible = true;
		return tile.GetData ();
	}
	
	public void SetData (int data)
	{
		tile.SetData (data);
	}
	
	public Vec2 GetPoint ()
	{
		return tile.GetPoint ();
	}
	
	public void OnVisibleAreaMoved (float x, float y, float w, float h)
	{
		Vec2 point = tile.GetPoint ();
		
		if (point.x >= w || point.y >= h ||
			(point.x + MapUtil.TILE_LENGTH) <= x ||
			(point.y + MapUtil.TILE_LENGTH <= y)) {
			visible = false;
		} else {
			visible = true;
		}
	}
}