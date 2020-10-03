using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : DynamicObjectController {
    int health = 3;
    public bool isDead;

    List<GameObject> hurtBy = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    Transform attackTarget;
    float aggroRange = 5;

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
}
