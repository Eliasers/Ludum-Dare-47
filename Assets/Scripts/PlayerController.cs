using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    List<GameObject> nearbyItems = new List<GameObject>();
    GameObject nearestItem;
    GameObject heldItem;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapArea((Vector2)transform.position + new Vector2(-0.1f, 0), (Vector2)transform.position + new Vector2(0.1f, -0.1f), StaticStuff.SolidLayers);
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

                if (Input.GetButtonDown("PickUp")) {
                    PickUp();
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

        if (nearbyItems.Count > 0) {
            nearbyItems.OrderBy(item => (item.transform.position - transform.position).magnitude);
            nearestItem = nearbyItems[0];
        } else {
            nearestItem = null;
        }
    }

    void PickUp() {
        if (heldItem != null) {
            heldItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            heldItem.GetComponent<Collider2D>().enabled = true;
            heldItem.transform.parent = null;
            heldItem.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            heldItem = null;
        } else if (nearestItem != null) {
            heldItem = nearestItem;
            heldItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            heldItem.GetComponent<Collider2D>().enabled = false;
            heldItem.transform.parent = transform;
            heldItem.transform.localPosition = new Vector2(0.6f, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Item")) {
            nearbyItems.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (nearbyItems.Contains(collision.gameObject)) {
            nearbyItems.Remove(collision.gameObject);
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
