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

    bool hasAttacked;
    float timeAttacking;

    float attackResolutionTime = 0.2f;
    float attackRecoveryTime = 0.6f;
    Vector2 attackOffset = new Vector2(0.4f, 0.5f);

    State state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapArea((Vector2)transform.position + new Vector2(-0.2f, 0), (Vector2)transform.position + new Vector2(0.2f, -0.1f), StaticStuff.SolidLayers);
            //(transform.position, 0.1f, StaticStuff.SolidLayers) != null;
        anim.SetBool("Grounded", grounded);

        if (state == State.Moving)
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
                state = State.Attacking;
                anim.Play("playerAttack");
                timeAttacking = 0;
                hasAttacked = false;
            }
        } else if (state == State.Attacking) {
            //Attack logic
            timeAttacking += Time.deltaTime;

            rb.velocity = new Vector2(rb.velocity.x / Mathf.Pow(10, Time.deltaTime), rb.velocity.y);

            if (timeAttacking >= attackResolutionTime)
            {
                if (!hasAttacked)
                {
                    Collider2D[] r = new Collider2D[5];
                    ContactFilter2D cf = new ContactFilter2D();
                    cf.layerMask = StaticStuff.Hurtables;
                    cf.useLayerMask = true;
                    Physics2D.OverlapCircle((Vector2)transform.position + attackOffset, 0.5f, cf, r);
                    for (int i = 0; i < r.Length; i++)
                    {
                        if (r[i] != null && r[i].gameObject != gameObject)
                        {
                            Debug.Log("Hit " +  r[i].gameObject.name);
                            r[i].GetComponent<NPCController>().TakeDamage(gameObject, 1);
                        }
                    }


                    hasAttacked = true;
                }
                
                if (timeAttacking >= attackRecoveryTime){
                    state = State.Moving;
                }
            }

        }
    }

    enum State { Moving, Attacking, Staggered }
}
