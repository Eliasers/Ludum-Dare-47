using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectController : MonoBehaviour
{
    public int age = 0;
    public List<Sprite> sprites;

    public virtual void PassTime()
    {
        age++;
    }
}
