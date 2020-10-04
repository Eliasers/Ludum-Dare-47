using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class NPCController : DynamicObjectController {
    public Species species;

    public bool female;

    public NPCController mother;
    public NPCController father;

    public Color hairColor = new Color(1, 1, 1, 1);
    public Color skinColor = new Color(1, 1, 1, 1);

    int health = 3;

    public NPCActivity activity;

    State state;
    float xMove;
    bool grounded;

    bool hasAttacked;
    float timeAttacking;

    float attackResolutionTime = 0.2f;
    float attackRecoveryTime = 0.6f;
    Vector2 attackOffset = new Vector2(0.4f, 0.5f);

    float timeStaggered;

    List<GameObject> hurtBy = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    Transform attackTarget;
    float aggroRange = 5;


    NPCController mate; //Current partner
    bool hasMated; //(This cycle)

    protected Rigidbody2D rb;
    protected Animator anim;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        grounded = Physics2D.OverlapArea((Vector2)transform.position + new Vector2(-0.2f, 0), (Vector2)transform.position + new Vector2(0.2f, -0.1f), StaticStuff.SolidLayers);
        anim.SetBool("Grounded", grounded);

        switch (state) {
            case State.Moving:

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
                                if (r[i] != null && r[i].gameObject != gameObject) {
                                    if (r[i].GetComponent<NPCController>() != null) {
                                        r[i].GetComponent<NPCController>().TakeDamage(gameObject, 1);
                                    } else if (r[i].GetComponent<DestructibleObjectController>() != null) {
                                        r[i].GetComponent<DestructibleObjectController>().Break();
                                    } else if (r[i].GetComponent<PlayerController>() != null) {
                                        r[i].GetComponent<PlayerController>().TakeDamage(1);
                                    }
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

    public override void PassTime() {
        base.PassTime();

        if (age > 3) {
            Die();
        }
    }

    public void TakeDamage(GameObject instigator, int amount) {
        health -= amount;

        if (health <= 0) {
            Die();
        } else {
            state = State.Staggered;
            anim.Play("playerStagger");
            timeStaggered = 0;

            if (hurtBy.Contains(instigator)) {
                enemies.Add(instigator);
                attackTarget = instigator.transform;
            } else {
                hurtBy.Add(instigator);
            }
        }
    }

    void Attack() {
        state = State.Attacking;
        anim.Play("playerAttack");
        timeAttacking = 0;
        hasAttacked = false;
    }

    public void Die() {
        state = State.Dead;
    }

    void FindMate() {
        if (female || age == 0) { throw new System.Exception("Invalid father"); }

        List<NPCController> fellows = new List<NPCController>(FindObjectsOfType<NPCController>());

        fellows.RemoveAll(fellow => fellow.age == 0 || fellow.female == false);

        fellows.OrderByDescending(fellow => fellow.health / fellow.age);


        foreach (NPCController fellow in fellows) {
            if ((transform.position - fellow.transform.position).magnitude < Random.Range(15, 45)) {
                fellow.ReceiveMate(this);
            }
        }
    }

    public void ReceiveMate(NPCController mate) {
        if (!female || age == 0) { throw new System.Exception("Invalid mother"); }
        int difficulty = 1;

        difficulty += age * 3 + mate.age;
        difficulty -= (health + mate.health);

        if (Random.Range(1, 6) > difficulty) {
            NPCController child = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Person.prefab"), transform.position, Quaternion.identity, null).GetComponent<NPCController>();

            child.age = 0;

            child.female = Random.Range(0, 1) == 1;

            child.species = species;
            
            child.mother = this;
            child.father = mate;

            switch (Random.Range(0, 3)) {
                case 0:
                    child.hairColor = hairColor;
                    child.skinColor = skinColor;
                    break;
                case 1:
                    child.hairColor = mate.hairColor;
                    child.skinColor = skinColor;
                    break;
                case 2:
                    child.hairColor = hairColor;
                    child.skinColor = mate.skinColor;
                    break;
                case 3:
                    child.hairColor = mate.hairColor;
                    child.skinColor = mate.skinColor;
                    break;
                default:
                    break;
            }

        }
    }

    public enum Species { Human }

    public enum NPCActivity { Vibing, Fighting, Override }

    enum State { Moving, Attacking, Staggered, Dead }
}