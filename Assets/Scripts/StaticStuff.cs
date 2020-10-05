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
    public static LayerMask Destructibles
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
        GameObject.FindObjectOfType<WorldState>().AddKarma(amount);
    }

    public static void EndLifeCycle() {
        Debug.Log("Ending cycle");
        GameObject.FindObjectOfType<WorldState>().PassTime();
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

public enum CharacterState { Moving, Attacking, Staggered }
}
