using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectController : DynamicObjectController
{
    public void Break() {
        Destroy(gameObject);
    }
}
