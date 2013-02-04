using UnityEngine;

public class App : MonoBehaviour
{
	[SerializeField]
	private TileView prefab;
	[SerializeField]
	private Light sceneLight;
	private float userDragSpeed;
	private TileServer server;
	private TileStorage storage;
	private int networkLatency = 50;//milliseconds
	private IMap map;
	private VisibleArea area;
	private bool initialized = false;
	private string fileName;
	
	public void Initialize (AppConfig config)
	{
		if (map != null) {
			map.Dispose ();
		}
		
		fileName = config.MapFileName;
		userDragSpeed = config.UserDragSpeed;
		
		server = new TileServer (config.MapSize.x, config.MapSize.y, prefab.GetPrefabCount (), config.RefreshIntervalSeconds, networkLatency);
		
		ITileFactory factory;
		if (config.RefreshTiles) {
			factory = new TimedProxyTileFactory (server, config.RefreshIntervalSeconds);
		} else {
			factory = new ProxyTileFactory (server);
		}
		
		ITileLoadingStrategy tileLoadingStrategy;
		if (config.LoadPotentialTiles) {
			tileLoadingStrategy = new EagerLoadingStrategy ();
		} else {
			tileLoadingStrategy = new LazyLoadingStrategy ();
		}
		
		storage = new TileStorage (factory);
		
		Map simpleMap = new Map (storage.Load (fileName), factory, tileLoadingStrategy, prefab, config.MapSize.x, config.MapSize.y);
		
		if (config.LimitTilesCount) {
			map = new LimitedMap (simpleMap, config.MaxTilesCount);
		} else {
			map = simpleMap;
		}
		
		area = new VisibleArea (map);
		
		initialized = true;
	}
	
	private void Update ()
	{
		if (initialized) {
			map.Update ();
			//mouse control
			if (Input.GetMouseButton (0)) {
				float x = Input.GetAxis ("Mouse X");
				float y = Input.GetAxis ("Mouse Y");
			
				area.Move (x * userDragSpeed, y * userDragSpeed);
			}
			//touch input
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 delta = Input.GetTouch (0).deltaPosition;
				
				area.Move (-delta.x * userDragSpeed, -delta.y * userDragSpeed);
			}
		}
	}
	
	private void OnEnable ()
	{
		if (initialized) {
			sceneLight.enabled = true;
		}
	}
	
	private void OnDisable ()
	{
		if (initialized) {
			sceneLight.enabled = false;
		}
	}
	
	private void OnApplicationQuit ()
	{
		if (initialized) {
			storage.Save (fileName, map.GetAllTiles ());
		}
	}
}