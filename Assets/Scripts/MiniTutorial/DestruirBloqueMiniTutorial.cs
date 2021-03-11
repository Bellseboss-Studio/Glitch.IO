using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirBloqueMiniTutorial : MonoBehaviour
{
    private Shake shake;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shake.CamaraShake();
            Destroy(this.gameObject);
        }

    }

}
