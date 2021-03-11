using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarColorAlGolpear : MonoBehaviour
{

    public bool esColor = false;
    private SpriteRenderer colorCursor;

    // Start is called before the first frame update
    void Start()
    {
        colorCursor = GetComponent<SpriteRenderer>();

        colorCursor.material.SetColor("_Color", Color.white);
    }

    // Update is called once per frame
    void cambiarColorAlHacerDaño()
    {
        if (esColor==true)
        {
            colorCursor.material.SetColor("_Color", Color.red);
            esColor = false;
        }
        else
        {
            colorCursor.material.SetColor("_Color", Color.white);
            esColor = true;
        }
    }
}
