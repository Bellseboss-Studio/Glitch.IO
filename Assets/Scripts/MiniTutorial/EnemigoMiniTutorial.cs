using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMiniTutorial : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    private UnityEngine.Object ReferenciaExplosion;
    private Shake shake;
    int contadorVidas = 0;
    bool meHanPegado = false;
    // Start is called before the first frame update
    void Start()
    {

        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        ReferenciaExplosion = Resources.Load("CursorExplosion");
    }

    // Update is called once per frame
    void MePegan()
    {
        if (contadorVidas == 1)
        {
            meHanPegado = true;
        }
        if (contadorVidas == 2)
        {
            meHanPegado = true;
        }
        if (contadorVidas == 3)
        {
            meHanPegado = true;
            GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);

            Destroy(gameObject);
        }

        if (meHanPegado == true)
        {
            shake.CamaraShake();
        }
    }
    private void OnCollisionEnter2D(Collision2D colisionMouse)
    {
        if (colisionMouse.gameObject.tag == "Player")
        {
            contadorVidas++;
            sr.material = matWhite;
            MePegan();

        }

    }
    private void OnCollisionExit2D(Collision2D colisionMouse)
    {
        if (colisionMouse.gameObject.tag == "Player")
        {
            Invoke("ResetMaterial", .1f);
        }
    }
    void ResetMaterial()
    {
        sr.material = matDefault;
    }
}
