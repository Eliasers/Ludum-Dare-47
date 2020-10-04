using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    CircleCollider2D trigger;
    public float detectionRange = 3;

    Animator anim;
    public GameObject textObjPrefab;
    TextMeshPro text;

    public float textYOffset;
    public float textXOffset;
    public bool repeatLines;
    public string fallBackLine;
    public List<string> voiceLines;

    bool touchingPlayer;
    float counter;
    public float secondsPerLetter = 0.2f;

    private void Awake() {
        textObjPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/TextBubble.prefab");
    }

    void Start()
    {
        GameObject textObj = Instantiate(textObjPrefab, new Vector3(textXOffset, textYOffset, 0), Quaternion.identity);
        textObj.transform.SetParent(transform, false);

        anim = textObj.GetComponent<Animator>();

        text = textObj.GetComponent<TextMeshPro>();

        trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.isTrigger = true;
        trigger.radius = detectionRange;
    }

    private void Update() {

        if (counter < 1) {
            anim.SetBool("Fade Out", true);
            anim.SetBool("Fade In", false);

            if (counter <= 0 && touchingPlayer && anim.GetCurrentAnimatorStateInfo(0).IsName("Empty")) {
                if (voiceLines.Count > 0) {
                    text.text = voiceLines[0];
                    if (repeatLines == true) {
                        voiceLines.Add(voiceLines[0]);
                    }
                    voiceLines.RemoveAt(0);

                    DisplayLine();
                } else text.text = fallBackLine;
            }
        }


        if (counter > 0) counter -= Time.deltaTime;
    }

    void DisplayLine() {
        anim.SetBool("Fade In", true);
        anim.SetBool("Fade Out", false);
        Debug.Log(Mathf.Sqrt(text.text.Length));
        counter = Mathf.Sqrt(text.text.Length) * 5 * secondsPerLetter;
        //anim.speed = 1 / (text.text.Length * (secondsPerLetter/3));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            touchingPlayer = true;

            if (voiceLines.Count == 0) {
                text.text = fallBackLine;
                DisplayLine();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            touchingPlayer = false;
        }
    }
}
