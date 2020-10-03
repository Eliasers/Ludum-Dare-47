using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    
    Rigidbody2D rb;
    Animator anim;

    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(transform.position, 0.25f, StaticStuff.solidLayers) != null;
        anim.SetBool("Grounded", grounded);

        float xMove = Input.GetAxis("Horizontal");
        anim.SetFloat("Abs X Move", Mathf.Abs(xMove));

        rb.velocity = new Vector2(xMove * movementSpeed, rb.velocity.y);

        if (xMove != 0) { transform.localScale = new Vector2((xMove > 0) ? 1 : -1, 1); }


        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
