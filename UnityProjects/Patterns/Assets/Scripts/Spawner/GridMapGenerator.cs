using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class GridMapGenerator : MonoBehaviour {
        [System.Serializable]
        public enum TileType { Empty, Floor, Wall }

        public class Tile {
            public Vector2Int gridPos;
            public TileType type;
            public GameObject instance;
        }

        public Tile[,] tiles;
    
        [Header("Randomization")]
        public bool useSeed;
        public int seed;

        public void GenerateGridMap(int width, int height, float wallChance = 0.2f) {
            if (useSeed) {
                Random.InitState(seed);
            }
            else {
                Random.InitState(System.Environment.TickCount);
            }

            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Tile tile = new Tile {
                        gridPos = new Vector2Int(x, y),
                        type = (Random.value < wallChance) ? TileType.Wall : TileType.Floor
                    };
                    tiles[x, y] = tile;
                }
            }
            EnsureConnected(); // make sure it's traversable
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
            // Mark unreachable floor tiles as walls
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (tiles[x, y].type == TileType.Floor && !visited.Contains(new Vector2Int(x, y)))
                    tiles[x, y].type = TileType.Wall;
        }
    }
}
