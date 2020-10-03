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

    bool isAttacking;
    bool hasAttacked;
    float timeAttacking;

    float attackResolutionTime = 0.2f;
    float attackRecoveryTime = 0.36f;
    Vector2 attackOffset = new Vector2(0.7f, 0.7f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(transform.position, 0.25f, StaticStuff.SolidLayers) != null;
        anim.SetBool("Grounded", grounded);

        if (!isAttacking)
        {
            float xMove = Input.GetAxis("Horizontal");
            anim.SetFloat("Abs X Move", Mathf.Abs(xMove));

            rb.velocity = new Vector2(xMove * movementSpeed, rb.velocity.y);

            if (xMove != 0) { transform.localScale = new Vector2((xMove > 0) ? 1 : -1, 1); }


            if (Input.GetButtonDown("Jump") && grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                isAttacking = true;
            }
        } else {
            //Attack logic
            timeAttacking += Time.deltaTime;

            if (timeAttacking >= attackResolutionTime)
            {
                if (!hasAttacked)
                {
                    Collider2D[] r = new Collider2D[5];
                    ContactFilter2D cf = new ContactFilter2D();
                    cf.layerMask = StaticStuff.Hurtables;
                    cf.useLayerMask = true;
                    Physics2D.OverlapCircle(transform.position, 0.1f, cf, r);
                    for (int i = 0; i < r.Length; i++)
                    {
                        if (r[i] != null && r[i].gameObject != gameObject)
                        {
                            Debug.Log(r[i].gameObject.name);
                            r[i].GetComponent<NPCController>().TakeDamage(gameObject, 1);
                        }
                    }


                    hasAttacked = true;
                }
            }

        }
    }
}
