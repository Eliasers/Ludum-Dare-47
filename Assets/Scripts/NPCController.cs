using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : DynamicObjectController
{
    public bool isDead;

    public Sprite corpse;

    public override void PassTime()
    {
        base.PassTime();

        if (age > 3)
        {
            Die();
        }
    }

    public void Die(){
        isDead = true;
        GetComponent<SpriteMask>().sprite = null;
    }

    public void Die(Sprite corpse)
    {
        isDead = true;
        GetComponent<SpriteMask>().sprite = corpse;
    }
}
