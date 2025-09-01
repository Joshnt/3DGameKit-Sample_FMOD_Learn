using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitDefinition
{
    public string surfaceType;                // e.g. "Wood", "Metal"
    public List<Material> materials = new();  // all materials that belong to this surface
}

[CreateAssetMenu(fileName = "SurfaceDatabase", menuName = "Game/Hit Database")]
public class HitDatabase : ScriptableObject
{
    public List<HitDefinition> hits = new();

    /// <summary>
    /// Returns the surfaceType string for a given material.
    /// If no match is found, returns null.
    /// </summary>
    public string GetHitType(Material mat)
    {
        foreach (var hit in hits)
        {
            foreach (var m in hit.materials)
            {
                if (m == mat)
                {
                    return hit.surfaceType;
                }
            }
        }
        return null;
    }
}
