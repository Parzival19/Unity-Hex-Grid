using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : MonoBehaviour
{

    private bool HasRoad;
    private bool HasHouse;
    
    private HexUtility.HexCoordinates coord;
    private HexUtility.HexBiome biome;

    public HexInfo(int x, int y, HexUtility.HexBiome hexBiome)
    {
        HasRoad = false;
        HasHouse = false;

        coord = new HexUtility.HexCoordinates(x, y);
        biome = hexBiome;
        Debug.Log(this.ToString());

    }

    public void BuildRoad()
    {
        HasRoad = true;
    }

    public void BuildHouse()
    {
        HasHouse = true;
    }

    public HexUtility.HexBiome GetBiome()
    {
        return biome;
    }

    public HexUtility.HexCoordinates GetCoord()
    {
        return coord;
    }

    public override string ToString()
    {

        return ("(" + coord.X + "," + coord.Z + ")" + " : " + biome);
    }
}
