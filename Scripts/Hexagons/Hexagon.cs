using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Builds the mesh for the Hexagon, and colors it in depending on the HexInfo's biome. Then gives the hexObject that we have created the HexInfo component
/// </summary>
public class Hexagon
{

    private GameObject hexObject;
    private HexInfo hexInfo;

    public Hexagon(int x, int y, HexUtility.HexBiome biome)
    {

        hexObject = new GameObject("(" + x + ", " + y + ")");
        if((y & 1) == 0)
        {
            GetHexTransform().position = new Vector3(x * HexUtility.outerRadius * 2 + HexUtility.outerRadius, 0, y * (HexUtility.outerRadius + HexUtility.innerRadius));

        }
        else
        {
            GetHexTransform().position = new Vector3(x * HexUtility.outerRadius * 2, 0, y * (HexUtility.outerRadius + HexUtility.innerRadius));

        }

        // Adds the Hex Info script to the hex
        hexInfo = hexObject.AddComponent<HexInfo>();

        hexInfo = new HexInfo(x, y, biome);

        CreateHexagon();

        
    }

    /**
     * Creates the Hexagon at this position
     */
    private void CreateHexagon()
    {
        MeshRenderer meshRenderer = hexObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        MeshFilter meshFilter = hexObject.AddComponent<MeshFilter>();

        // creates the new mesh
        Mesh mesh = new Mesh
        {
            vertices = HexUtility.corners,

            triangles = HexUtility.triangles,

            uv = HexUtility.uv
        };

        // recalculates normals
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        hexObject.GetComponent<MeshRenderer>().material.color = HexUtility.GetHexColor(hexInfo.GetBiome());
    }

    public GameObject GetHexObject()
    {
        return hexObject;
    }

    public Transform GetHexTransform()
    {
        return hexObject.transform;
    }

    public HexUtility.HexCoordinates GetCoord()
    {
        return hexInfo.GetCoord();
    }

    public HexUtility.HexBiome GetBiome()
    {
        return hexInfo.GetBiome();
    }

    public HexInfo GetHexInfo()
    {
        return hexInfo;
    }

}
