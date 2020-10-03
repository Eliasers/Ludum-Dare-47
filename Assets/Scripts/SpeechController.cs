using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    BoxCollider2D trigger;
    public float detectionRangeX, detectionRangeY;

    Animator anim;
    public GameObject textObjPrefab;
    TextMeshPro text;

    public string fallBackLine;
    public List<string> voiceLines;

    bool touchingPlayer;
    int counter;
    float framesPerLetter = 30;

    private void Awake() {
        textObjPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TextBubble.prefab");
    }

    void Start()
    {
        GameObject textObj = Instantiate(textObjPrefab);
        textObj.transform.SetParent(transform, false);

        anim = textObj.GetComponent<Animator>();

        text = textObj.GetComponent<TextMeshPro>();

        trigger = gameObject.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(detectionRangeX, detectionRangeY);
    }

    private void Update() {
        if (counter == 0 && touchingPlayer) {
            if (voiceLines.Count > 0) {
                text.text = voiceLines[0];
                voiceLines.RemoveAt(0);
            }
            else text.text = fallBackLine;
            anim.SetTrigger("fadeIn");
            Debug.Log("SAS");
            counter = (int)(text.text.Length * framesPerLetter);
            anim.speed = 1 / (text.text.Length / 6f);
        }

        if (counter > 0) counter--;
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
}
