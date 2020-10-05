﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : NPCController
{
    public GameObject rock;
    SpeechController speech;
    CircleCollider2D trigger;

    bool isAlive = true;
    bool isStuck = true;
    bool deathRevealed;

    private void Start() {
        base.Start();
        speech = GetComponent<SpeechController>();
    }

    public override void PassTime()
    {
        base.PassTime();

        speech.voiceLines.Clear();

        //Dies
        if (isAlive == true && rock != null)
        {
            isAlive = false;
            Destroy(speech);
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
            transform.position = new Vector3(16, -2, 0);
            anim.SetTrigger("Sit");
            speech.fallBackLine = "Man I'm tired";
            //speech.voiceLines.Add("Thank you kind sir!");
        }
    }

    private void Update() {
        if (isAlive && isStuck && rock == null) {
            anim.SetTrigger("StandUp");
            speech.fallBackLine = "I'll always be in your debt!";
            speech.voiceLines.Add("Thank you kind sir!");
            StaticStuff.AddKarma(10);
            isStuck = false;
        }

        //Not working anymo :(
        if (!isAlive && !deathRevealed && Vector2.Distance(player.transform.position, transform.position) < 8) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }
    }
}