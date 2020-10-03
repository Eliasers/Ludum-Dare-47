using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticStuff
{
    public static LayerMask solidLayers
    {
        get
        {
            return LayerMask.GetMask(new string[] { "Terrain" });
        }
    }
}
