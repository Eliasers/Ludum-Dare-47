using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldState : MonoBehaviour
{
    enum flagID { LOL1, LOL22
                , NumberOfFlags}

    static double karma = 0;

    public GameObject KarmaIncrease, KarmaDecrease;

    DynamicScenery[] dynamicObjs;

    bool[] flag = new bool[(int)flagID.NumberOfFlags];

    private void Start() {
        KarmaIncrease = GameObject.Find("KarmaUpText");
        KarmaDecrease = GameObject.Find("KarmaDownText");

        dynamicObjs = GameObject.FindObjectsOfType<DynamicScenery>();
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

        if (supressNotification == false) {
            KarmaIncrease.GetComponent<Animator>().SetTrigger("fadeIn");
            Debug.Log("klick");
        }
    }

    public void PassTime() {

    }
}
