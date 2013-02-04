TileMap

This is a simple 2D tile map project. A map contains a 2-dimension arrays of tiles. Each tile contain an integer variable which is used to index a tree prefab. Each tree is created on a plane of 10 meters wide and 10 meters long. The plane represents a tile in the map. Whenever the user scrolls to move his viewport, the map creates new tiles if necessary.

The App is the entry point where a map can be created using an AppConfig. The AppConfig contains a bunch of variables that the user can use to configure the map's behavior.

There are two types of maps: a normal map (Map) that contains a list of tiles, and another map (LimitedMap) that tracks the time of the visible tiles and then stores the tiles with longest time.

There are three types of tiles: a normal tile (Tile), a ProxyTile that only loads the data when the tile is visible, and a TimedProxyTile that refreshes at the specified rate. The creation of tile are encapsulated in ITileFactory, so that the map doesn't know the implementation details of its tiles.

The ProxyTile uses two different strategies to load data: a LazyLoadingStrategy to retrieve the data of the tile that's visible, while the EagerLoadingStrategy loads additional tiles based on user's input.

Tile's storage is based on the .NET serialization and deserializaiton.

The VisibleArea reads the user input and updates the map and camera.