using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float distance;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = -player.position * distance/1000;
        transform.localPosition = new Vector3(pos.x, pos.y, 10);
    }
}
