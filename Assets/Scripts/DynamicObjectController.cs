using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectController : MonoBehaviour
{
    public int age;
    public virtual void PassTime()
    {
        age++;
    }
}
