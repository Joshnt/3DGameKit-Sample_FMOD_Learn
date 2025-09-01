using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SurfaceDefinition
{
    public string surfaceType;                // e.g. "Wood", "Metal"
    public List<Material> materials = new();  // all materials that belong to this surface
}

[CreateAssetMenu(fileName = "SurfaceDatabase", menuName = "Game/Surface Database")]
public class SurfaceDatabase : ScriptableObject
{
    public List<SurfaceDefinition> surfaces = new();
    public string defaultSurface = "Earth";

    /// <summary>
    /// Returns the surfaceType string for a given material.
    /// If no match is found, returns null.
    /// </summary>
    public string GetSurfaceType(Material mat)
    {
        foreach (var surface in surfaces)
        {
            foreach (var m in surface.materials)
            {
                if (m == mat)
                {
                    return surface.surfaceType;
                }
            }
        }
        return defaultSurface;
    }
}
