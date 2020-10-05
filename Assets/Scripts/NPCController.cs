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

    protected virtual void Update() {
        if (!isAlive && !deathRevealed && Vector2.Distance(player.transform.position, transform.position) < 8) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }
    }

    public override void PassTime() {
        base.PassTime();
        ResetSpeech();
    }

    public virtual void Die(bool keepAnim = false) {
        Destroy(speech);
        Destroy(GetComponent<CircleCollider2D>());
        isAlive = false;
        try {
            Destroy(transform.GetChild(0).gameObject);
        } catch (System.Exception e) {
            Debug.Log("Dying npc had no children: " + e.Message);
        }

        if (!keepAnim) {
            Destroy(anim);
            spr.sprite = corpse;
        }
    }

    protected void ResetSpeech() {
        if (speech == null) return;
        speech.counter = 0;
    }
}