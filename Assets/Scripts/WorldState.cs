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

    public void AddKarma(double amount) {
        karma += amount;
    }

    private void Start() {
        tx.color = new Color(1, 0.5f, 0, 1);
    }

    private void Update() {
        tx.color = new Color(tx.color.r, tx.color.g, tx.color.b, tx.color.a * 0.99f);
        Debug.Log(tx.color);
    }
}
