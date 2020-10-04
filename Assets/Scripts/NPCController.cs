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
    public bool isDead;

    List<GameObject> hurtBy = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    Transform attackTarget;
    float aggroRange = 5;


    NPCController mate; //Current partner
    bool hasMated; //(This cycle)

    Rigidbody2D rb;
    Animator anim;

    private void Start() {
        
    }

    private void Update() {
        
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
            if (hurtBy.Contains(instigator)) {
                enemies.Add(instigator);
                attackTarget = instigator.transform;
            } else {
                hurtBy.Add(instigator);
            }
        }
    }

    public void Die() {
        isDead = true;
    }

    public void Die(Sprite corpse) {
        isDead = true;
        GetComponent<SpriteMask>().sprite = corpse;
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
}