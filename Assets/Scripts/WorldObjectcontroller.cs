using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectController : DynamicObjectController
{
    

    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();

    }
}
