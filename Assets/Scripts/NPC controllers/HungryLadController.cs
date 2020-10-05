using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryLadController : NPCController
{
    bool hungry = true;

    protected override void Start() {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Chowbeet") && hungry) {
            Destroy(collision.gameObject);

            StaticStuff.AddKarma(10);

            hungry = false;
            speech.voiceLines = new List<string> { "Wowie, thanks! That is one thick beet.", "I'm chowing down on this yummy snack this instant." };
            speech.fallBackLine = "I don't know if I'll ever be hungry again!";

            ResetSpeech();
        }

        if (!deathRevealed && !isAlive && Vector2.Distance(player.transform.position, transform.position) < 10) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }
    }

    public override void PassTime() {
        base.PassTime();
        if (hungry) {
            switch (age) {
                case 1:

                    break;
                default:
                    break;
            }
        }
    }
}
