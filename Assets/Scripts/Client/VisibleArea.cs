using UnityEngine;

public class VisibleArea
{
	private IMap map;

	/// <summary>
	/// The pivot which is the lower left of the visible area (w, h).
	/// </summary>
	private Vector2 pivot;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="VisibleArea"/> class.
	/// </summary>
	public VisibleArea (IMap map)
	{
		this.map = map;
		this.pivot = Vector2.zero;
		
		Move (0, 0);
	}
	
	public void Move (float dx, float dy)
	{
		Vec2 mapSize = map.GetSize ();
		Vector2 cameraSize = GetCameraSize ();
		
		pivot.x = Mathf.Clamp (pivot.x + dx, 0, mapSize.x * MapUtil.TILE_LENGTH - cameraSize.x * 2);
		pivot.y = Mathf.Clamp (pivot.y + dy, 0, mapSize.y * MapUtil.TILE_LENGTH - cameraSize.y * 2);
		
		UpdateMap ();
	}
	/*
	public void DrawGizmos ()
	{
		Vector2 cameraSize = GetCameraSize ();
		Vector3 position = Camera.main.transform.position;// + Camera.main.far / 2 * Vector3.down;
		
		//Gizmos.DrawWireCube (position, new Vector3 (cameraSize.x * 2, Camera.main.far, cameraSize.y * 2));
	}
	*/
	private void UpdateMap ()
	{
		UpdateCamera ();
		
		Vector2 cameraSize = GetCameraSize ();
		
		map.SetVisibleRegion (pivot.x, pivot.y, pivot.x + cameraSize.x * 2, pivot.y + cameraSize.y * 2);
	}
	
	private void UpdateCamera ()
	{
		Vector2 cameraSize = GetCameraSize ();
		Vector3 position = Camera.main.transform.position;
		
		position.x = pivot.x + cameraSize.x;
		position.z = pivot.y + cameraSize.y;
		
		Camera.main.transform.position = position;
	}
	
	private Vector2 GetCameraSize ()
	{
		Vector2 size = Vector2.zero;
		
		size.x = Camera.main.orthographicSize * Camera.main.aspect;
		size.y = Camera.main.orthographicSize;
		
		return size;
	}
}