using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    
    Rigidbody2D rb;
    Animator anim;

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

        if (xMove != 0) { transform.localScale = new Vector2((xMove > 0) ? 1 : -1, 1); }

        anim.SetFloat("Abs X Move", Mathf.Abs(xMove));

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(jumpForce * Vector2.up);
        }
    }
}
