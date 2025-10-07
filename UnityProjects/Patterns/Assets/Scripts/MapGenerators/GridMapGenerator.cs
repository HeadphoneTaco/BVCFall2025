using System.Collections.Generic;
using UnityEngine;

public class GridMapGenerator : MonoBehaviour {
    [System.Serializable]
    public enum TileType { Empty, Floor, Wall, Obstacle }

    public class Tile {
        public Vector2Int gridPos;
        public TileType type;
        public GameObject instance;
    }

    public Tile[,] tiles;

    public void GenerateGridMap(int width, int height, float obstacleChance = 0.2f) {
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile tile = new Tile {
                    gridPos = new Vector2Int(x, y),
                    type = (Random.value < obstacleChance) ? TileType.Wall : TileType.Floor
                };
                tiles[x, y] = tile;
            }
        }

        EnsureConnected();
    }

    private void EnsureConnected() {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        Vector2Int? start = null;
        for (int x = 0; x < width && start == null; x++)
            for (int y = 0; y < height && start == null; y++)
                if (tiles[x, y].type == TileType.Floor)
                    start = new Vector2Int(x, y);

        if (start == null) return;

        var visited = new HashSet<Vector2Int>();
        var queue = new Queue<Vector2Int>();
        queue.Enqueue(start.Value);
        visited.Add(start.Value);

        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0) {
            var current = queue.Dequeue();

            foreach (var dir in dirs) {
                Vector2Int next = current + dir;
                if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height)
                    continue;
                if (tiles[next.x, next.y].type != TileType.Floor)
                    continue;
                if (visited.Add(next))
                    queue.Enqueue(next);
            }
        }

        // Mark unreachable floors as walls
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (tiles[x, y].type == TileType.Floor && !visited.Contains(new Vector2Int(x, y)))
                    tiles[x, y].type = TileType.Wall;
    }
}
