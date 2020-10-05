using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryLadController : NPCController
{
    bool hungry = true;

    protected override void Start() {
        base.Start();

        speech.voiceLines = new List<string> { "Jolly morning to you, sir!", "I'm having a grand old day, and I sure hope you are too!" };
        speech.fallBackLine = "Howdy!";
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
        if (hungry && isAlive) {
            switch (age) {
                case 1:
                    speech.voiceLines = new List<string> { "Hmm... I'm peckish, got a snack to share?" };
                    speech.fallBackLine = "I could really go for something to chew on.";
                    break;
                case 2:
                    speech.voiceLines = new List<string> { "Dammit, I'm hungry. How long can you guys go without eating?", "Sure you don't have anything? I'm in something of a pickle here." };
                    speech.fallBackLine = "Mmm... Pickle...";
                    break;
                case 3:
                    speech.voiceLines = new List<string> { "I'm literally starving over here!", "You're looking like a snack...", "Oh, who am I kidding, there's no way I could kill any of you even if I were willing too..." };
                    speech.fallBackLine = "Please, sir? I'm dying...";
                    break;
                case 4:
                    Die();
                    break;
                default:
                    break;
            }
        }
    }
}
