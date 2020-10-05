using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LadderControl : MonoBehaviour
{
    public GameObject target;
    GameObject player;
    TextMesh txt;

    Color hidden = new Color(1, 1, 1, 0);
    Color visible = new Color(1, 1, 1, 1);

    bool inRange = false;

    void Start() {
        player = GameObject.Find("Player");
        txt = GetComponentInChildren<TextMesh>();
        txt.color = hidden;
    }

    void Update() {
        if (inRange && Input.GetButtonDown("Enter")) {
            player.transform.position = target.transform.position;
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
