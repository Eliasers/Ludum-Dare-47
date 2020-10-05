using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyController : NPCController
{
    public Sprite destroyedMudman;

    public void MudmanDestroyed() {
        StaticStuff.RemoveKarma(10);
        GetComponent<SpeechController>().fallBackLine = "*quiet sobbing*";
        speech.Clear();
        speech.voiceLines.Add("My... My mudman!");
        ResetSpeech();
        GetComponent<Animator>().SetTrigger("Sad");
        Destroy(this);
    }
}