using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds onto all hexagons in the hexmap 2d array. Also constructs the world according to height and width.
/// </summary>
public class WorldCreator : MonoBehaviour
{
    public int height, width;
    public static Hexagon[,] hexmap;

    // Start is called before the first frame update
    void Start()
    {

        hexmap = new Hexagon[width, height];

        ConstructWorld();
    }

    private void ConstructWorld()
    {

        GameObject HexHolder = new GameObject("Hex's");

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Hexagon hex = new Hexagon(x, y, HexUtility.GetRandomHexBiome());
                hex.GetHexTransform().SetParent(HexHolder.transform);

                hexmap[x, y] = hex;
            }
        }
    }
}
