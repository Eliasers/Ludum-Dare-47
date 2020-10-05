using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using Unity.Mathematics;

public class NPCController : DynamicObjectController {

    protected Animator anim;
    protected GameObject player;
    protected SpeechController speech;
    protected SpriteRenderer spr;
    public Sprite corpse;

    protected bool isAlive = true;

    protected bool deathRevealed;

    protected virtual void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        speech = GetComponent<SpeechController>();
        spr = GetComponent<SpriteRenderer>();
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

    public virtual void Die() {
        Destroy(speech);
        Destroy(GetComponent<CircleCollider2D>());
        isAlive = false;
        anim.SetBool("Is Alive", false);
    }
}