using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldState : MonoBehaviour
{
    enum flagID { LOL1, LOL22
                , NumberOfFlags}

    static double karma = 0;

    public Text tx;

    bool[] flag = new bool[(int)flagID.NumberOfFlags];

    public void AddKarma(double amount, bool supressNotification = false) {
        karma += amount;

        if(supressNotification == false) {
            tx.GetComponent<Animator>().SetTrigger("fadeIn");
            Debug.Log("klick");
        }
    }

    private void Update() {
        
    }
}
