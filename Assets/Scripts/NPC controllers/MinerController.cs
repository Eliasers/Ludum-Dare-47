using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : DynamicObjectController
{
    public GameObject rock;
    SpeechController speech;

    bool isAlive = true;

    private void Start() {
        speech = GetComponent<SpeechController>();

    }

    public override void PassTime()
    {
        base.PassTime();

        if (rock != null)
        {
            isAlive = false;
            StaticStuff.RemoveKarma(10);
        }
    }
}