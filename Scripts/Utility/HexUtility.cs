using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexUtility
{
    public enum HexBiome
    {
        Grass, Forest, Hilly, Ocean, Desert, Mountain, White = -1 
    }

    public enum HexDirection
    {
        SW, W, NW, NE, E, SE
    }

    public static readonly int HexBiomeSize = 6;

    public struct HexCoordinates
    {

        public int X;
        public int Z;

        public HexCoordinates(int x, int z)
        {
            X = x;
            Z = z;
        }
    }


    public const float outerRadius = 10f;

    public const float innerRadius = outerRadius / 2;

    public static int[] triangles = 
        {
            1,5,0,
            1,4,5,
            1,2,4,
            2,3,4
        };

    public static Vector3[] corners = {
            new Vector3(-outerRadius , 0, -innerRadius),
            new Vector3(-outerRadius, 0, innerRadius),
            new Vector3(0f, 0, outerRadius),
            new Vector3(outerRadius, 0, innerRadius),
            new Vector3(outerRadius, 0, -innerRadius),
            new Vector3(0f, 0, -outerRadius)
    };

    public static Vector2[] uv =
    {
            new Vector3(-outerRadius, 0, -innerRadius),
            new Vector3(-outerRadius, 0, innerRadius),
            new Vector3(0f, 0, outerRadius),
            new Vector3(outerRadius, 0, innerRadius),
            new Vector3(outerRadius, 0, -innerRadius),
            new Vector3(0f, 0, -outerRadius)
    };

    public static Color GetHexColor(HexBiome colorIndex)
    {
        switch(colorIndex){
            case HexBiome.Grass: // Grasslands
                return new Color32(75, 184, 20, 1);
            case HexBiome.Forest: // Forest
                return new Color32(0, 100, 10, 1);
            case HexBiome.Hilly: // Hilly
                return new Color32(122, 245, 61, 1);
            case HexBiome.Ocean: // Ocean
                return new Color32(61, 138, 245, 1);
            case HexBiome.Desert: // Desert
                return new Color32(245, 200, 60, 1);
            case HexBiome.Mountain: // Mountains
                return new Color32(85, 100, 79, 1);
            default: // not one of these means its white
                return Color.white;
        }
    }

    public static HexBiome GetHexBiomeFromInt(int hexBiome)
    {
        switch (hexBiome)
        {
            case 0: // Grasslands
                return HexBiome.Grass;
            case 1: // Forest
                return HexBiome.Forest;
            case 2: // Hilly
                return HexBiome.Hilly;
            case 3: // Ocean
                return HexBiome.Ocean;
            case 4: // Desert
                return HexBiome.Desert;
            case 5: // Mountains
                return HexBiome.Mountain;
            default: // not one of these means its white
                return HexBiome.White;
        }
    }

    public static HexBiome GetRandomHexBiome()
    {
        return GetHexBiomeFromInt(Random.Range(0, HexBiomeSize));
    }

    /**
     * GetHexagonsCorners does just what it says it does, it returns an array of Vectors 
     * for hexagon at the position given, by adding every vector in corners to the position
     */
    public static Vector3[] GetHexagonsCorners(Vector3 position)
    {
        Vector3[] v = new Vector3[corners.Length];

        for (int i = 0; i < corners.Length; i++)
        {
            v[i] = corners[i] + position;
        }

        return v;
    }

    /**
     * Checks for the hex to the left, if there is a hex (X greater than 0) then it returns that hex. 
     * Otherwise it returns itself.
     */
    public static Hexagon GetLeftHexagon(Hexagon hex)
    {
        if(hex.GetCoord().X > 0)
        {
            return WorldCreator.hexmap[hex.GetCoord().X, hex.GetCoord().Z];
        }

        return hex;
    }

    /**
     * Checks for the hex to the right, if there is a hex (X less than length - 1) then it returns that hex.
     * Otherwise it returns itself.
     */
    public static Hexagon GetRightHexagon(Hexagon hex)
    {
        if (hex.GetCoord().X < WorldCreator.hexmap.GetLength(0) - 1)
        {
            return WorldCreator.hexmap[hex.GetCoord().X + 1, hex.GetCoord().Z];
        }

        return hex;
    }

    /**
     * Checks for the hex to the top left, if there is a hex (Z less than length - 1) && (Z & 1 == 0) || ((Z & 1 == 1) && (X greater than 0) then it returns that hex
     * otherwise it returns itself
     */
    public static Hexagon GetTopLeftHexagon(Hexagon hex)
    {
        if(hex.GetCoord().Z < WorldCreator.hexmap.GetLength(1) - 1) { 
            if((hex.GetCoord().Z & 1) == 0)
            {
                return WorldCreator.hexmap[hex.GetCoord().X, hex.GetCoord().Z + 1];
            }
            else if(hex.GetCoord().X  > 0)
            {
                return WorldCreator.hexmap[hex.GetCoord().X - 1, hex.GetCoord().Z + 1];
            }
        } 


        return hex;
    }

    /**
     * Checks for the hex to the top right, if there is a hex (Z less than length - 1) && ((Z & 1 == 0) && (X Less than Length) || ((Z & 1 == 1)) then it returns that hex
     * otherwise it returns itself
     */
    public static Hexagon GetTopRightHexagon(Hexagon hex)
    {
        if (hex.GetCoord().Z < WorldCreator.hexmap.GetLength(1) - 1)
        {
            if ((hex.GetCoord().Z & 1) == 0)
            {
                if (hex.GetCoord().X < WorldCreator.hexmap.GetLength(0)) // Need -1?
                {
                    return WorldCreator.hexmap[hex.GetCoord().X + 1, hex.GetCoord().Z + 1];
                }
            }
            else 
            {
                return WorldCreator.hexmap[hex.GetCoord().X, hex.GetCoord().Z + 1];
            }
        }


        return hex;
    }

    /**
     * Returns the bottom left hexagon if there is one. Otherwise it returns itself 
     */
    public static Hexagon GetBottomLeftHexagon(Hexagon hex)
    {

        if(hex.GetCoord().Z > 0)
        {
            if((hex.GetCoord().Z & 1) == 0)
            {
                return WorldCreator.hexmap[hex.GetCoord().X, hex.GetCoord().Z - 1];
            } else if (hex.GetCoord().X > 0)
            {
                return WorldCreator.hexmap[hex.GetCoord().X - 1, hex.GetCoord().Z - 1];
            }
        }
        return hex;
    }

    /**
     * Returns the bottom right hexagon if there is one. Otherwise it returns itself 
     */
    public static Hexagon GetBottomRightHexagon(Hexagon hex)
    {

        if (hex.GetCoord().Z > 0)
        {
            if ((hex.GetCoord().Z & 1) == 0)
            {
                if (hex.GetCoord().X < WorldCreator.hexmap.GetLength(0)) // need -1?
                {
                    return WorldCreator.hexmap[hex.GetCoord().X + 1, hex.GetCoord().Z - 1];
                }
            }
            else 
            {
                return WorldCreator.hexmap[hex.GetCoord().X, hex.GetCoord().Z - 1];
            }
        }
        return hex;
    }

    public static Hexagon GetHexagonInDirection(Hexagon hex, HexDirection direct)
    {
        switch(direct)
        {
            case HexDirection.SW:
                return GetBottomLeftHexagon(hex);
            case HexDirection.W:
                return GetLeftHexagon(hex);
            case HexDirection.NW:
                return GetTopLeftHexagon(hex);
            case HexDirection.NE:
                return GetTopRightHexagon(hex);
            case HexDirection.E:
                return GetRightHexagon(hex);
            case HexDirection.SE:
                return GetBottomRightHexagon(hex);
            default:
                return null;
        }
    }

    /**
     * Returns the corner in that direction
     */
    public static Vector3 GetCorner(HexDirection direct)
    {
        return corners[(int)direct];
    }

    /**
     * Returns the next corner in that direction
     */
    public static Vector3 GetNextCorner(HexDirection direct)
    {
        return corners[(int)direct + 1];
    }
}

