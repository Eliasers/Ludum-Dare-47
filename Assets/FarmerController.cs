using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : NPCController
{

    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;

    bool rocksGone;
    bool gotToPlanting;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speech.voiceLines = new List<string> { "This roight 'ere would make a dandy spot to plant me some chowbeets...", "...if it weren't for all these bloody rocks!"};
        speech.fallBackLine = "Darned rocks...";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!rocksGone && rock1 == null && rock2 == null && rock3 == null) {
            speech.voiceLines = new List<string> { "You krumped them rocks? That's roight bloody sweet of ya!", "Oi'll get roight ta plantin'!" };
            speech.fallBackLine = "Stop buggin' me! Oi'm right 'boutta get ta work, oi am!";
        }
    }
}
