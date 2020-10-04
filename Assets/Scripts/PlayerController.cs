using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    
    Rigidbody2D rb;
    Animator anim;

    int health = 3;

    bool grounded;

    bool hasAttacked;
    float timeAttacking;

    float attackResolutionTime = 0.2f;
    float attackRecoveryTime = 0.6f;
    Vector2 attackOffset = new Vector2(0.4f, 0.5f);

    float timeStaggered;

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
        anim.SetBool("Grounded", grounded);

        switch (state) {
            case State.Moving:
                float xMove = Input.GetAxis("Horizontal");
                anim.SetFloat("Abs X Move", Mathf.Abs(xMove));

                rb.velocity = new Vector2(xMove * movementSpeed, rb.velocity.y);

                if (xMove != 0) { transform.localScale = new Vector2((xMove > 0) ? 1 : -1, 1); }


                if (Input.GetButtonDown("Jump") && grounded) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                if (Input.GetButtonDown("Fire1")) {
                    state = State.Attacking;
                    anim.Play("playerAttack");
                    timeAttacking = 0;
                    hasAttacked = false;
                }
                break;
            case State.Attacking:
                //Attack logic
                timeAttacking += Time.deltaTime;

                rb.velocity = new Vector2(rb.velocity.x / Mathf.Pow(10, Time.deltaTime), rb.velocity.y);

                if (timeAttacking >= attackResolutionTime) {
                    if (!hasAttacked) {
                        Collider2D[] r = new Collider2D[5];
                        ContactFilter2D cf = new ContactFilter2D();
                        cf.layerMask = StaticStuff.Hurtables;
                        cf.useLayerMask = true;
                        Physics2D.OverlapCircle((Vector2)transform.position + attackOffset, 0.25f, cf, r);
                        for (int i = 0; i < r.Length; i++) {
                            if (r[i] != null && r[i].gameObject != gameObject) {
                                if (r[i].CompareTag("Destructible")) {
                                    Destroy(r[i]);
                                }
                            }
                        }


                        hasAttacked = true;
                    }

                    if (timeAttacking >= attackRecoveryTime) {
                        state = State.Moving;
                    }
                }
                break;
            case State.Staggered:
                timeStaggered += Time.deltaTime;
                if (timeStaggered > 0.4f) {
                    state = State.Moving;
                }
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        } else {
            state = State.Staggered;
            anim.Play("playerStagger");
            timeStaggered = 0;
        }
    }

    public void Die() {
        //Implement. Reincarnate and pass time
    }

    enum State { Moving, Attacking, Staggered }
}
