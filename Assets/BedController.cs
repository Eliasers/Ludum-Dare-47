using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedController : MonoBehaviour {
    GameObject player;
    TextMesh txt;

    Color hidden = new Color(1, 1, 1, 0);
    Color visible = new Color(1, 1, 1, 1);

    bool inRange = false;

    float timer;
    public float deathTime = 6;
    bool dying;

    public GameObject youDiedPrefab;
    GameObject youDiedUI;

    void Start() {
        player = GameObject.Find("Player");
        txt = GetComponentInChildren<TextMesh>();
        txt.color = hidden;
    }

    void Update() {
        if (inRange && Input.GetButtonDown("Enter")) {
            timer = deathTime;
            dying = true;
            youDiedUI = Instantiate(youDiedPrefab);
            player.GetComponent<PlayerController>().state = PlayerController.State.Sleeping;
            player.transform.position = (Vector2)transform.localPosition + Vector2.up * 0.39f + (Vector2)transform.parent.position;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Animator>().Play("playerDead");
        }
        
        if (dying) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                dying = false;
                player.GetComponent<PlayerController>().Die(true);
                Destroy(youDiedUI);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            inRange = true;
            txt.color = visible;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            inRange = false;
            txt.color = hidden;
        }
    }
}
