using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScenery : DynamicObjectController
{
    SpriteRenderer spr;

    public SpriteProgression[] spriteProgression;

    private void Start() {
        spr = GetComponent<SpriteRenderer>();
    }

    public void PassTime(double karmaProgress) {
        base.PassTime();

        UpdateSprite(karmaProgress);
    }

    void UpdateSprite(double karmaProgress) {
        //Skip if only 1 sprite
        if (spriteProgression.Length > 1) {
            //Move up to the karma levels lower sprite
            int lowSprite = 0;
            while (lowSprite < spriteProgression.Length - 1) {
                //Check if there is still a sprite with a smaller limit
                if (spriteProgression[lowSprite + 1].limit < karmaProgress) {
                    lowSprite++;
                }
                else break;
            }
            
            //Is there not a sprite higher than the one selected?
            if (lowSprite == spriteProgression.Length - 1) spr.sprite = spriteProgression[lowSprite].sprite;
            //Shit gotta do math to interpolate between 2 sprites
            else {
                int highSprite = lowSprite + 1;

                double distanceToLow = Math.Abs(spriteProgression[lowSprite].limit - karmaProgress);
                double distanceToHigh = Math.Abs(spriteProgression[highSprite].limit - karmaProgress);

                double ratioHigh = distanceToHigh / (distanceToHigh + distanceToLow);

                double random = UnityEngine.Random.Range(0f,1f);
                
                if (random < ratioHigh) spr.sprite = spriteProgression[lowSprite].sprite;
                else spr.sprite = spriteProgression[highSprite].sprite;
            }
        }
    }

    [Serializable]
    public struct SpriteProgression
    {
        public Sprite sprite;
        public double limit;
    }
}
