using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaveGeneration : MonoBehaviour
{

    public int EMPTY = 0;
    public int GROUND = 1;
    public int CAVE = 2;
    public int GRASS = Tiles.Grass.id;
    public int ROCK = 4;

    [Header("Location")]
    public Vector2 offset = new Vector2(0, 0);

    [Header("Terrain Gen")]
    public int width;
    public int height;
    public int heightReduction = 2;
    public float smoothness;
    public float seed;

    [Header("Cave Gen")]
    [Range(0, 1)]
    public float caveModifier = 0.15f;

    [Header("Rock Gen")]
    [Range(0, 1)]
    public float rockModifier = 0.60f;

    [Header("Tiles")]
    public TileBase grassTile;
    public TileBase groundTile;
    public TileBase caveTile;
    public TileBase rockTile;
    public Tilemap groundTilemap;
    public Tilemap caveTilemap;

    public int[,] map;

    void Start()
    {
        Generate();
    }

    void Update()
    {
        
    }

    public void Generate()
    {
        // initializes our 2d array with a bunch 0s
        map = new int[width, height];
        // put 1s inside of our 2d array
        map = TerrainGeneration(map);
        RenderMap(map, groundTile, groundTilemap, caveTile, caveTilemap, grassTile, rockTile);
    }

    // Populates a 2d array with 1s using perlin noise
    public int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;
        for (int x = 0; x < width; x++)
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height / heightReduction);
            perlinHeight += height / heightReduction;
            for (int y = 0; y < perlinHeight; y++)
            {
                int rockValue = Mathf.RoundToInt(Mathf.PerlinNoise(x * rockModifier + seed, y * rockModifier + seed));
                int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise(x * caveModifier + seed, y * caveModifier + seed));
                map[x, y] = (caveValue == 1) ? CAVE : GROUND;
                map[x, y] = (rockValue == 1) ? map[x, y] : ROCK;
                border(map, x, y, perlinHeight);
            }
        }
        return map;
    }

    public void border(int[,] map, int x, int y, int perlinHeight)
    {
        if (y == perlinHeight - 1)
        {
            map[x, y] = GRASS;
            map[x, y - 1] = GROUND;
        }
    }


    public void RenderMap(int[,] map, TileBase groundTilebase, Tilemap groundTilemap, TileBase caveTilebase, Tilemap caveTilemap, TileBase grassTilebase, TileBase rockTilebase)
    {
        groundTilemap.ClearAllTiles();
        caveTilemap.ClearAllTiles();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == GROUND)
                {
                    groundTilemap.SetTile(new Vector3Int((int)(x + offset.x), (int)(y + offset.y), 0), groundTilebase);
                }
                if (map[x, y] == CAVE)
                {
                    caveTilemap.SetTile(new Vector3Int((int)(x + offset.x), (int)(y + offset.y), 0), caveTilebase);
                }
                if (map[x, y] == GRASS)
                {
                    groundTilemap.SetTile(new Vector3Int((int)(x + offset.x), (int)(y + offset.y), 0), grassTilebase);
                }
                if (map[x, y] == ROCK)
                {
                    groundTilemap.SetTile(new Vector3Int((int)(x + offset.x), (int)(y + offset.y), 0), rockTilebase);
                }
            }
        }
    }


}
