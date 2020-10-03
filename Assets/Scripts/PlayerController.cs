using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    
    Rigidbody2D rb;
    Animator anim;
    SpriteMask sm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(xMove * movementSpeed, rb.velocity.y);

        

        anim.SetFloat("Abs X Move", Mathf.Abs(xMove));
    }
}
