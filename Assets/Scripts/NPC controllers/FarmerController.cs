using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : NPCController {

    public GameObject chowbeetPrefab;

    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;

    public GameObject grass;
    public GameObject wheatLow;
    public GameObject wheatHigh;

    bool rocksGone;
    bool gotToPlanting;
    bool finished;

    Vector2 vibingSpot;
    Vector2[] farmingSpots;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        speech.voiceLines = new List<string> { "This roight 'ere would make a dandy spot to grow some wheat, and a couple o' chowbeets, why not...", "...if it weren't for all these bloody rocks!" };
        speech.fallBackLine = "Darned rocks...";

        vibingSpot = transform.position;
        farmingSpots = new Vector2[] { rock1.transform.position, rock2.transform.position, rock3.transform.position };
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        if (!rocksGone && rock1 == null && rock2 == null && rock3 == null) {
            rocksGone = true;
            StaticStuff.AddKarma(10);
            speech.voiceLines = new List<string> { "You krumped dem rocks? That's roight bloody sweet uv ya!", "Oi'll get roight ta plantin'!" };
            speech.fallBackLine = "Stop buggerin' me! Oi'm right 'boutta get ta work, oi am!";
            ResetSpeech();
        }
    }

    public override void PassTime() {
        base.PassTime();

        if (rocksGone && !gotToPlanting) {
            gotToPlanting = true;
            transform.position = farmingSpots[1];
            anim.Play("farmerFarm");
            speech.voiceLines = new List<string> { "This'll be a jolly good chowbeet and wheat field, just you wait!" };
            speech.fallBackLine = "*whistling*";

            wheatLow.SetActive(true);
            grass.SetActive(false);
        } else if (gotToPlanting && !finished) {
            finished = true;

            for (int i = 0; i < farmingSpots.Length; i++) {
                Instantiate(chowbeetPrefab, farmingSpots[i], Quaternion.identity);
            }

            wheatHigh.SetActive(true);
            wheatLow.SetActive(false);

            transform.position = vibingSpot;
            anim.Play("farmerIdle");
            speech.voiceLines = new List<string> { "Feast your eyes upon my magnificent field o' beets!", "Oi'd offa' some to the fella' who 'elped me out, but they unfortunately passed away recently. *sniff*", "Ya know, you remind me uv 'em, a bit. 'ave a beet or two, if ya loik. It's on me." };
            speech.fallBackLine = "This community would starve without me!";
        } else {
            speech.Clear();
            if (rocksGone) {
                if (StaticStuff.Karma < 20) {
                    speech.fallBackLine = "Da rottin' remains o' my comrades fertilize da soil, hehe.";
                } else if (StaticStuff.Karma < 40) {
                    speech.fallBackLine = "Ge' away from me, outsider, 'fore oi make kebab outta' yer innards!";
                } else {
                    speech.fallBackLine = "'ello, stranger! If you're peckish 'round 'ere, oi'm da guy you're lookin fer.";
                }
            }
        }


    }
}
