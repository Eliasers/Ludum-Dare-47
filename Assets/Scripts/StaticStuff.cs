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
            return LayerMask.GetMask(new string[] { "Destructible", "DestructibleNoCollision" });
        }
    }

    public static double Karma {
        get {
            return GameObject.FindObjectOfType<WorldState>().Karma;
        }
    }

    public static void RemoveKarma(float amount) {
        GameObject.FindObjectOfType<WorldState>().RemoveKarma(amount);
    }

    public static void AddKarma(float amount) {
        Debug.Log(GameObject.FindObjectOfType<WorldState>() == null);
        GameObject.FindObjectOfType<WorldState>().AddKarma(amount);
    }

    public static void EndLifeCycle() {
        Debug.Log("Ending cycle");
        GameObject.FindObjectOfType<WorldState>().PassTime();
    }

    public enum CharacterState { Moving, Attacking, Staggered }
}
