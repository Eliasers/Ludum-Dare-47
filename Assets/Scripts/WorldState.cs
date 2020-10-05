using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldState : MonoBehaviour
{
    enum flagID { LOL1, LOL22
                , NumberOfFlags}

    static double karma = 0;
    double karmaGoal = 100;

    int age = 0;

    public GameObject KarmaIncrease, KarmaDecrease;

    public double Karma {
        get {
            return karma;
        }
    }

    bool[] flag = new bool[(int)flagID.NumberOfFlags];

    private void Start() {
        KarmaIncrease = GameObject.Find("KarmaUpText");
        KarmaDecrease = GameObject.Find("KarmaDownText");
    }

    public void AddKarma(double amount, bool supressNotification = false) {
        Debug.Log("Karma increase");
        karma += amount;

        if(supressNotification == false) {
            KarmaIncrease.GetComponent<Animator>().SetTrigger("fadeIn");
        }
    }

    public void RemoveKarma(double amount, bool supressNotification = false) {
        Debug.Log("Karma decrease");
        karma -= amount;

        /*if (supressNotification == false) {
            KarmaIncrease.GetComponent<Animator>().SetTrigger("fadeIn");
        }*/
    }

    public void PassTime() {
        age++;

        DynamicScenery[] dynamicObjs = GameObject.FindObjectsOfType<DynamicScenery>();
        NPCController[] NPCOBjs = GameObject.FindObjectsOfType<NPCController>();
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();

        double progress = karma / karmaGoal;
        Debug.Log("NEW progress: " + progress);

        for (int i = 0; i < dynamicObjs.Length; i++) {
            dynamicObjs[i].PassTime(progress);
        }
        for (int i = 0; i < NPCOBjs.Length; i++) {
            NPCOBjs[i].PassTime();
        }
        player.Die();
    }
}
