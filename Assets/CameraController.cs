using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    float addVerticalOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset + addVerticalOffset * Vector3.up;
        if (Input.GetAxis("Vertical") < -0.5)
        {
            if (addVerticalOffset > -1)
            {
                addVerticalOffset -= 4 * Time.deltaTime;
            }
            else addVerticalOffset = -1;
        } else {
            if (addVerticalOffset < -0.5)
            {
                addVerticalOffset += 6 * Time.deltaTime;
            }
            else addVerticalOffset = 0;
        }
    }
}
