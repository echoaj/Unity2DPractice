using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public CaveGeneration cg;
    public static int axeStrength = 1;

    int grassID;
    int dirtID;
    int rockID;

    int grassDur;
    int dirtDur;
    int rockDur;

    private Vector4 tileHitCount = new Vector4(0, 0, -1, -1);       // [BlockType, hitCount, x, y]

    void Start()
    {
        grassID = cg.GRASS;
        dirtID = cg.GROUND;
        rockID = cg.ROCK;

        grassDur = 2;
        dirtDur = 3;
        rockDur = 5;
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))  // click
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // where we click
            int x = (int)(clickPosition.x - cg.offset.x);       // cordinates of click location
            int y = (int)(clickPosition.y - cg.offset.y);

            // Check if clicked within the map
            if (x >= 0 && x < cg.width && y >= 0 && y < cg.height)
            {
                int blockID = cg.map[x, y];
                if (blockID != cg.EMPTY)
                {
                    // check if same blockID as before
                    if (blockID == tileHitCount[0] && x == tileHitCount[2] && y == tileHitCount[3])
                    {
                        tileHitCount[1] += axeStrength;
                        Debug.Log(tileHitCount);
                        if (tileHitCount[0] == grassID && tileHitCount[1] >= grassDur)
                        {
                            destroyTile(x, y);
                        }
                        else if (tileHitCount[0] == dirtID && tileHitCount[1] >= dirtDur)
                        {
                            destroyTile(x, y);
                        }
                        else if (tileHitCount[0] == rockID && tileHitCount[1] >= rockDur)
                        {
                            destroyTile(x, y);
                        }
                    }
                    else
                    {
                        // Reset blockType and count
                        tileHitCount = new Vector4(blockID, 1, x, y);
                    }
                }
            }
        }
    }

    void destroyTile(int x, int y)
    {
        cg.map[x, y] = cg.CAVE;
        cg.RenderMap(cg.map, cg.groundTile, cg.groundTilemap, cg.caveTile, cg.caveTilemap, cg.grassTile, cg.rockTile);
        resetCount();
    }

    void resetCount()
    {
        tileHitCount[0] = 0;
        tileHitCount[1] = 0;
        tileHitCount[2] = -1;
        tileHitCount[3] = -1;
    }
}
