using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldState : MonoBehaviour
{
    static float karma = 50;
    float karmaGoal = 100;

    int age = 0;

    public GameObject KarmaIncrease, KarmaDecrease;

    GameObject fog;

    AudioSource src;
    public AudioClip di;
    public AudioClip happy;

    SpriteRenderer plxRenderer;

    //public AudioClip[] music;
    //public AudioSource musicPlayer;

    public double Karma {
        get {
            return karma;
        }
    }

    private void Start() {

        fog = GameObject.FindGameObjectWithTag("Fog");

        KarmaIncrease = GameObject.Find("KarmaUpText");
        KarmaDecrease = GameObject.Find("KarmaDownText");

        src = GetComponent<AudioSource>();

        plxRenderer = GameObject.Find("Parallax 1").GetComponent<SpriteRenderer>();

        UpdateColors(karma/karmaGoal);
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

        karma = Mathf.Clamp(karma, 0, 100);

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

        UpdateColors(progress);

        /*
        if (karma <= 0) {
            musicPlayer.clip = null;
        } else if (karma <= 10) {
            musicPlayer.clip = music[0];
        } else if (karma <= 30) {
            musicPlayer.clip = music[1];
        } else if (karma <= 50) {
            musicPlayer.clip = music[2];
        } else if (karma <= 70) {
            musicPlayer.clip = music[3];
        }*/
    }

    void UpdateColors(float progress) {
        plxRenderer.color = Color.Lerp(new Color(0.1f, 0.1f, 0.1f), Color.white, progress);

        Color a = new Color(65f / 255f, 89f / 255f, 72f / 255f);
        Color b = new Color(83f / 255f, 181f / 255f, 207f / 255f);
        Color bc = Color.Lerp(a, b, progress);
        Camera.main.backgroundColor = bc;
    }
}
