using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanController : NPCController
{
    public GameObject canon;
    public GameObject smokeEffect;

    bool canonTriggered = false;
    bool canonBoom = false;

    float canonTimer = 5;

    AudioSource canonAudio;
    public AudioClip canonPrimed, canonFire;  

    override protected void Start() {
        base.Start();

        canonAudio = canon.GetComponent<AudioSource>();
    }

    public override void PassTime() {
        base.PassTime();

        if(age == 1 && isAlive) {
            anim.SetTrigger("Leave");
            Die(true);
            deathRevealed = true;
        }
        else if(age == 1 && !canonTriggered) {
            anim.SetTrigger("Die");
            Die(true);
        }
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if(canonTriggered && !canonBoom) {
            canonTimer -= Time.deltaTime;
            if(canonTimer < 0) {
                canonAudio.clip = canonFire;
                canonAudio.Play();
                canonBoom = true;
                Instantiate(smokeEffect, canon.transform.position + new Vector3(-2.4f, 0.1f), Quaternion.identity);
                canon.GetComponent<Animator>().SetTrigger("Reset");


                Vector3 playerPos = GameObject.Find("Player").transform.position;
                //Ah shit he dead
                if (transform.position.x < playerPos.x && playerPos.x < canon.transform.position.x && Mathf.Abs(playerPos.y - transform.position.y) < 4) {
                    GameObject.Find("Player").GetComponent<PlayerController>().Die();
                    speech.voiceLines = new List<string> { "Good Heavens!" };
                    ResetSpeech();
                    StaticStuff.AddKarma(10);
                }
                //Ah shit me dead
                else {
                    anim.SetTrigger("Die");
                    Die(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!canonTriggered && collision.gameObject.CompareTag("Player")) {
            canonTriggered = true;
            canon.GetComponent<Animator>().SetTrigger("Fire");
            canonAudio.clip = canonPrimed;
            canonAudio.Play();
        }
    }
}
