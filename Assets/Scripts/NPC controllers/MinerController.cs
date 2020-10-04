using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : NPCController
{
    public GameObject rock;
    SpeechController speech;
    CircleCollider2D trigger;
    GameObject player;

    bool isAlive = true;
    bool isStuck = true;
    bool deathRevealed;

    private void Start() {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        speech = GetComponent<SpeechController>();
    }

    public override void PassTime()
    {
        base.PassTime();

        //Dies
        if (isAlive == true && rock != null)
        {
            isAlive = false;
            trigger = new CircleCollider2D();
            trigger.isTrigger = true;
            trigger.radius = 10;
            Destroy(speech);
        }
        
        //Working
        if (isAlive && age == 1) {
            speech.fallBackLine = "Work Work Work";
            speech.voiceLines.Add("What have you done for the world recently?");
            speech.voiceLines.Add("Digging for the greater good!");
        }

        //Not working :(
        if (!deathRevealed && Vector2.Distance(player.transform.position, transform.position) < 10) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }

        //Work done
        if (isAlive && age == 2) {
            speech.fallBackLine = "I'll always be in your debt!";
            speech.voiceLines.Add("Thank you kind sir!");
        }
    }

    private void Update() {
        if (isStuck && rock == null) {
            anim.SetTrigger("StandUp");
            speech.fallBackLine = "I'll always be in your debt!";
            speech.voiceLines.Add("Thank you kind sir!");
            StaticStuff.AddKarma(10);
            isStuck = false;
        }
    }
}