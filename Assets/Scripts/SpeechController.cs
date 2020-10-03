using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    BoxCollider2D trigger;
    public float detectionRangeX, detectionRangeY;

    Animator anim;
    public GameObject textObjPrefab;
    Text text;

    public string fallBackLine;
    public List<string> voiceLines;

    bool showingText;
    bool touchingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject textObj = Instantiate(textObjPrefab);
        textObj.transform.parent = transform;

        anim = textObj.GetComponent<Animator>();

        text = textObj.GetComponent<Text>();
        text.text = "ASDF";

        trigger = gameObject.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(detectionRangeX, detectionRangeY);
    }

    private void Update() {
        if (showingText == false && touchingPlayer) {
            if (voiceLines.Count > 0) {
                text.text = voiceLines[0];
                voiceLines.RemoveAt(0);
            }
            else text.text = fallBackLine;
            anim.SetTrigger("fadeIn");
            showingText = true;
            Debug.Log("SAS");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            touchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            touchingPlayer = false;
        }
    }

    public void ShowTextDone() {
        showingText = false;
        Debug.Log("RESET " + showingText);
    }
}
