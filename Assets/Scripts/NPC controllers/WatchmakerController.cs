﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchmakerController : NPCController
{
    public bool hasClockium;

    bool sick;

    public GameObject clockUIPrefab;

    protected override void Start() {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Clockium")) {
            Destroy(collision.gameObject);

            StaticStuff.AddKarma(10);

            hasClockium = true;
            speech.voiceLines = new List<string> { "Heavens, that's a huge hunk of clockium!", "I'm set for a lifetime!", "In the not-so-distant future, everyone in the region will have a watch.", "You're an angel." };
            if (!sick) speech.fallBackLine = "I love making watches.";
            else speech.fallBackLine = "Must. Keep. Working...";

            ResetSpeech();
        }

        if (!deathRevealed && !isAlive && Vector2.Distance(player.transform.position, transform.position) < 10) {
            StaticStuff.RemoveKarma(10);
            deathRevealed = true;
        }
    }

    public override void PassTime() {
        base.PassTime();

        if (isAlive) {
            speech.Clear();

            if (hasClockium) {
                Instantiate(clockUIPrefab).GetComponent<Canvas>().worldCamera = Camera.main;
                speech.voiceLines = new List<string>();
            }

            if (StaticStuff.Karma < 50) {
                if (!sick) {
                    sick = true;
                    if (!hasClockium) {
                        speech.voiceLines = new List<string> { "*cough* *cough*", "Oh, don't worry about me. I've just caught some seasonal nastiness, or something.", "Do you have any clockium, perchance? I need some for the watch I'm making." };
                        speech.fallBackLine = "*cough*";
                    } else {
                        speech.voiceLines = new List<string> { "*cough* *cough*", "Oh, don't worry about me. I've just caught some seasonal nastiness, or something." };
                        speech.fallBackLine = "Must. Keep. Working...";
                    }
                } else {
                    Die();
                }
            }
        }
    }
}
