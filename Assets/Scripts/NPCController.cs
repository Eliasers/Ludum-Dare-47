using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class NPCController : DynamicObjectController {

    protected Animator anim;
    protected GameObject player;
    protected SpeechController speech;

    protected bool isAlive;

    protected bool deathRevealed;

    protected void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        speech = GetComponent<SpeechController>();
    }

    protected void Update() {
        if (!isAlive && !deathRevealed && Vector2.Distance(player.transform.position, transform.position) < 8) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }
    }

    public override void PassTime() {
        base.PassTime();
    }

    public void Die() {
        Destroy(speech);
        isAlive = false;
        anim.SetBool("Is Alive", false);
    }
}