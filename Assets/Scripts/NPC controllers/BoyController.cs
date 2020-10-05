using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyController : MonoBehaviour
{
    public Sprite destroyedMudman;

    public void MudmanDestroyed() {
        StaticStuff.RemoveKarma(10);
        GetComponent<SpeechController>().fallBackLine = "*quiet sobbing*";
        GetComponent<Animator>().SetTrigger("Sad");
        Destroy(this);
    }
}
