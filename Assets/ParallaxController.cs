using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float distance;
    public Transform player;

    Vector2 offset;

    void Start() {
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = -player.position * distance/1000;
        transform.localPosition = (Vector3)(offset + new Vector2(pos.x, pos.y)) + Vector3.forward * 10;
    }
}
