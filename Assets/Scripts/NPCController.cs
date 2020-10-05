using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class NPCController : DynamicObjectController {

    protected Animator anim;
    protected GameObject player;

    protected void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void PassTime() {
        base.PassTime();
    }

    public void Die() {
        //?
    }
}