using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanController : NPCController
{
    public GameObject canon;

    bool canonTriggered = false;
    bool canonBoom = false;

    float canonTimer = 5;

    AudioSource canonAudio;
    public AudioClip canonPrimed, canonFire;  

    override protected void Start() {
        base.Start();

        canonAudio = canon.GetComponent<AudioSource>();
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

                Vector3 playerPos = GameObject.Find("Player").transform.position;
                //Ah shit he dead
                if (transform.position.x < playerPos.x && playerPos.x < canon.transform.position.x && Mathf.Abs(playerPos.y - transform.position.y) < 4) {
                    GameObject.Find("Player").GetComponent<PlayerController>().Die();
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
