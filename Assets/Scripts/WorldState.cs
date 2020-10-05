using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldState : MonoBehaviour
{
    enum flagID { LOL1, LOL22
                , NumberOfFlags}

    static float karma = 0;
    float karmaGoal = 100;

    int age = 0;

    public GameObject KarmaIncrease, KarmaDecrease;

    GameObject fog;

    AudioSource src;
    public AudioClip di;
    public AudioClip happy;

    public double Karma {
        get {
            return karma;
        }
    }

    bool[] flag = new bool[(int)flagID.NumberOfFlags];

    private void Start() {

        fog = GameObject.FindGameObjectWithTag("Fog");

        KarmaIncrease = GameObject.Find("KarmaUpText");
        KarmaDecrease = GameObject.Find("KarmaDownText");

        src = GetComponent<AudioSource>();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("SKIP");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Die();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            AddKarma(10);
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            RemoveKarma(10);
        }
    }

    public void AddKarma(float amount, bool supressNotification = false) {
        Debug.Log("Karma increase");
        karma += amount;

        src.clip = happy;
        src.Play();

        /*
        if(supressNotification == false) {
            KarmaIncrease.GetComponent<Animator>().SetTrigger("fadeIn");
        }*/
    }

    public void RemoveKarma(float amount, bool supressNotification = false) {
        Debug.Log("Karma decrease");
        karma -= amount;

        src.clip = di;
        src.Play();

        /*if (supressNotification == false) {
            KarmaIncrease.GetComponent<Animator>().SetTrigger("fadeIn");
        }*/
    }

    public void PassTime() {
        age++;

        DynamicScenery[] dynamicObjs = GameObject.FindObjectsOfType<DynamicScenery>();
        NPCController[] NPCOBjs = GameObject.FindObjectsOfType<NPCController>();
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();

        float progress = karma / karmaGoal;
        Debug.Log("NEW progress: " + progress);
        /*
        SpriteRenderer fogRenderer = fog.GetComponent<SpriteRenderer>();
        if (karma < 0) {
            fogRenderer.enabled = true;
            fogRenderer.color = new Color(1, 1, 1, -progress);
        } else {
            fogRenderer.enabled = false;
        }*/

        for (int i = 0; i < dynamicObjs.Length; i++) {
            dynamicObjs[i].PassTime(progress);
        }
        for (int i = 0; i < NPCOBjs.Length; i++) {
            NPCOBjs[i].PassTime();
        }
    }
}
