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

    public float textYOffset;
    public float textXOffset;
    public bool repeatLines;
    public string fallBackLine;
    public List<string> voiceLines;

    bool touchingPlayer;
    int counter;
    float framesPerLetter = 150;

    private void Awake() {
        textObjPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TextBubble.prefab");
    }

    void Start()
    {
        GameObject textObj = Instantiate(textObjPrefab, transform.position + new Vector3(textXOffset, textYOffset, 0), Quaternion.identity);
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
                if(repeatLines == true) {
                    voiceLines.Add(voiceLines[0]);
                    voiceLines.RemoveAt(0);
                }
                else voiceLines.RemoveAt(0);

            }
            else text.text = fallBackLine;
            anim.SetTrigger("fadeIn");
            Debug.Log("SAS");
            counter = (int)(text.text.Length * framesPerLetter);
            anim.speed = 1 / (text.text.Length / (framesPerLetter / 12));
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
