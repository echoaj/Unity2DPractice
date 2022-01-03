using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceduralGeneration : MonoBehaviour
{
    public int width, height;
    public GameObject dirt, grass, stone;
    private int offsetX;
    private int offsetY;

    void Start()
    {
        offsetX = width / 2;
        offsetY = height / 2;
        Generation();
    }

    void Generation()
    {

        for (int x = -offsetX; x < width-offsetX; x++)//This will help spawn a tile on the x axis
        {

            // Basic procedural generation to gradually increase and decrease height value
            int minHeight = height - 1;
            int maxHeight = height + 2;
            // Random range will give us a random height above or below previous height
            height = Random.Range(minHeight, maxHeight);

            int minStoneDist = height - offsetY - 3;
            int maxStoneDist = height - offsetY - 4;
            int stoneDist = Random.Range(minStoneDist, maxStoneDist);


            for (int y = -offsetY; y < height-offsetY; y++)//This will help spawn a tile on the y axis
            {
                if(y > stoneDist)
                    spawnObj(dirt, x, y);
                else
                    spawnObj(stone, x, y);
            }
            spawnObj(grass, x, height-offsetY);
        }
    }

    void spawnObj(GameObject obj, int width, int height)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
