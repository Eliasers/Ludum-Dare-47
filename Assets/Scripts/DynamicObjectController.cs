using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectController : MonoBehaviour
{
    public int age = 0;

    public virtual void PassTime()
    {
        age++;
    }
}
