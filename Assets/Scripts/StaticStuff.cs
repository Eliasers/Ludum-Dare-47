using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticStuff
{
    public static LayerMask SolidLayers
    {
        get
        {
            return LayerMask.GetMask(new string[] { "Terrain" });
        }
    }
    public static LayerMask Hurtables
    {
        get
        {
            return LayerMask.GetMask(new string[] { "Player", "NPC" });
        }
    }

    public enum CharacterState { Moving, Attacking, Staggered }
}
