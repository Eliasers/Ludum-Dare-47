using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaterialGenerator : MonoBehaviour {

    Color hairColor;
    Color skinHighlight;
    Color skin;
    Color skinShadow;
    public Color trousers;
    Color trousersShadow;
    public Color shirt;
    Color shirtShadow;

    // Start is called before the first frame update
    void Start()
    {
        LoadGenes(GetComponent<NPCController>());

        GenerateClothes();

        GenerateMaterial();
    }

    void LoadGenes(NPCController c) {

        hairColor = c.hairColor;

        skin = c.skinColor;
        skinHighlight = c.skinColor + new Color(0.1f, 0.1f, 0.1f);
        skinShadow = c.skinColor - new Color(0.1f, 0.1f, 0.1f);

    }

    void GenerateClothes() {
        trousersShadow = trousers - new Color(0.1f, 0.1f, 0.1f);
        shirtShadow = shirt - new Color(0.1f, 0.1f, 0.1f);
    }

    void GenerateMaterial() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        Texture2D t = new Texture2D(256, 1, TextureFormat.RGB24, false);

        for (int i = 0; i < 256; i++) {
            if (i > 235) {
                t.SetPixel(i, 1, hairColor);
            } else if (i > 220) {
                t.SetPixel(i, 1, skinHighlight);
            } else if (i > 180) {
                t.SetPixel(i, 1, skin);
            } else if (i > 160) {
                t.SetPixel(i, 1, skinShadow);
            } else if (i > 140) {
                t.SetPixel(i, 1, trousers);
            } else if (i > 120) {
                t.SetPixel(i, 1, trousersShadow);
            } else if (i > 100) {
                t.SetPixel(i, 1, shirt);
            } else {
                t.SetPixel(i, 1, shirtShadow);
            }
        }

        t.Apply();

        sr.material.SetTexture("_SwapTex", t);
    }
}
