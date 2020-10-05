using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchmakerController : NPCController
{
    public bool hasClockium;
    public SpeechController sc;

    bool sick;

    private void Start() {
        base.Start();
        sc = GetComponent<SpeechController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Clockium")) {
            Destroy(collision.gameObject);

            StaticStuff.AddKarma(10);

            hasClockium = true;
            sc.voiceLines = new List<string> { "Heavens, that's a huge hunk of clockium!", "I'm set for a lifetime!", "In the not-so-distant future, everyone in the region will have a watch.", "You're an angel." };
            if (!sick) sc.fallBackLine = "I love making watches.";
            else sc.fallBackLine = "Must. Keep. Working...";
        }

        if (!deathRevealed && !isAlive && Vector2.Distance(player.transform.position, transform.position) < 10) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }
    }

    public override void PassTime() {
        base.PassTime();
        if (StaticStuff.Karma < 0) {
            if (!sick) {
                sick = true;
                if (!hasClockium) {
                    sc.voiceLines = new List<string> { "*cough* *cough*", "Oh, don't worry about me. I've just caught some seasonal nastiness, or something.", "Do you have any clockium, perchance? I need some for the watch I'm making." };
                    sc.fallBackLine = "*cough*";
                } else {
                    sc.voiceLines = new List<string> { "*cough* *cough*", "Oh, don't worry about me. I've just caught some seasonal nastiness, or something." };
                    sc.fallBackLine = "Must. Keep. Working...";
                }
            } else {
                Die();
            }
        }
    }
}
