﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : NPCController
{
    public GameObject rock;

    bool isStuck = true;

    public GameObject excavatedStuff;

    protected override void Start() {
        base.Start();
    }

    public override void PassTime()
    {
        base.PassTime();

        speech.voiceLines.Clear();

        //Dies
        if (isAlive == true && rock != null)
        {
            Die();
        }
        
        //Working
        if (isAlive && age == 1) {
            transform.position = new Vector3(19.5f, -0.4f, 0);
            anim.SetTrigger("Mine");
            speech.fallBackLine = "Work Work Work";
            speech.voiceLines.Add("What have you done for the world recently?");
            speech.voiceLines.Add("Digging for the greater good!");
        }

        //Work done
        if (isAlive && age == 2) {
            Destroy(GameObject.Find("MinerExcv"));
            Destroy(GameObject.Find("MinerTree"));
            transform.position = new Vector3(16, -2, 0);
            anim.SetTrigger("Sit");
            speech.fallBackLine = "Man I'm tired";
            //speech.voiceLines.Add("Thank you kind sir!");

            excavatedStuff.SetActive(true);
        }
    }

    protected override void Update() {
        base.Update();
        if (isAlive && isStuck && rock == null) {
            anim.SetTrigger("StandUp");
            speech.fallBackLine = "I'll always be in your debt!";
            speech.voiceLines = new List<string> { "Thank you kind sir!" };
            ResetSpeech();
            StaticStuff.AddKarma(10);
            isStuck = false;
        }
    }
}