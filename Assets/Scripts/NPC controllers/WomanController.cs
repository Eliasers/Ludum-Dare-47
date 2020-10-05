using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanController : NPCController
{
    public GameObject canon;

    bool canonTriggered = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!canonTriggered && collision.gameObject.CompareTag("Player")) {
            canonTriggered = true;
            canon.GetComponent<Animator>().SetTrigger("Fire");
            canon.GetComponent<AudioSource>().Play();
        }
    }
}
