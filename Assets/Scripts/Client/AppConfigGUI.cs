using System;
using UnityEngine;

public class AppConfigGUI : MonoBehaviour
{
	private App app;
	private AppConfig config = new AppConfig ();
	private bool showConfig;
	
	private void Awake ()
	{
		if (app = GetComponent<App> ()) {
			app.Initialize (config);
		}
	}
	
	private void OnGUI ()
	{
		if (app) {
			if (showConfig) {
				DrawConfiguration ();
			} else if (GUILayout.Button ("CONFIG")) {
				showConfig = true;
				app.enabled = false;
			}
		}
	}
	
	private void DrawConfiguration ()
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("User Drag Speed: " + (int)config.UserDragSpeed);
		GUILayout.Space (30);
		config.UserDragSpeed = GUILayout.HorizontalSlider (config.UserDragSpeed, 1f, 5f, GUILayout.Width (100), GUILayout.Height (20));
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		
		config.RefreshTiles = GUILayout.Toggle (config.RefreshTiles, "Refresh Tiles");
		GUI.enabled = config.RefreshTiles;
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Refresh Interval: " + config.RefreshIntervalSeconds + " sec");
		GUILayout.Space (30);
		config.RefreshIntervalSeconds = (int)GUILayout.HorizontalSlider (config.RefreshIntervalSeconds, 3, 20, GUILayout.Width (100), GUILayout.Height (20));
		GUILayout.EndHorizontal ();
		GUI.enabled = true;
		
		config.LimitTilesCount = GUILayout.Toggle (config.LimitTilesCount, "Limit Tiles Count");
		GUI.enabled = config.LimitTilesCount;
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Max Tile Count: " + config.MaxTilesCount);
		GUILayout.Space (30);
		config.MaxTilesCount = (int)GUILayout.HorizontalSlider (config.MaxTilesCount, 20, 200, GUILayout.Width (100), GUILayout.Height (20));
		GUILayout.EndHorizontal ();
		GUI.enabled = true;
		
		config.LoadPotentialTiles = GUILayout.Toggle (config.LoadPotentialTiles, "Load Potential Tiles");
		GUI.enabled = config.LoadPotentialTiles;
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Potential Tile Columns: " + config.PotentialTileColumns);
		GUILayout.Space (30);
		config.PotentialTileColumns = (int)GUILayout.HorizontalSlider (config.PotentialTileColumns, 1, 10, GUILayout.Width (100), GUILayout.Height (20));
		GUILayout.EndHorizontal ();
		GUI.enabled = true;
		
		if (GUILayout.Button ("OK")) {
			if (app) {
				app.Initialize (config);
				app.enabled = true;
				showConfig = false;
			}
		}
	}
}