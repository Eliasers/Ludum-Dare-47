using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class NPCController : DynamicObjectController {

    protected Animator anim;

    protected void Start() {
        anim = GetComponent<Animator>();
    }

    public override void PassTime() {
        base.PassTime();
    }

    public void Die() {
        //?
    }
}