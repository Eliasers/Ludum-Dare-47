using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerController : NPCController
{
    public GameObject rock;

    public override void PassTime()
    {
        base.PassTime();

        if (rock != null)
        {
            Die();
        }
    }
}
