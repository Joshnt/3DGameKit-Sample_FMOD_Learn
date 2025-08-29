using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SurfaceTypes
{
    Stone,
    Grass,
    Earth,
    Puddle,
    Metal

}

public class SurfaceType : MonoBehaviour
{
    public SurfaceTypes surfaceTypeName = SurfaceTypes.Earth;
}
