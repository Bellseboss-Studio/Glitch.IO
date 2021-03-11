using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptoresMecanica : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    MeshRenderer sr;
    private Shake shake;

    public int contadorVidas;
    private UnityEngine.Object ReferenciaExplosion;
    public VidaBoss vidaBoss;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<MeshRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;

        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        ReferenciaExplosion= Resources.Load("CursorExplosion");

    }

    void OnTriggerEnter(Collider collision)
    {
        //Output the Collider's GameObject's name
        //Debuug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            vidaBoss.vidaJefe--;
            vidaBoss.cambiarSpriteVida();
            contadorVidas++;
            sr.material = matWhite;
            MePegan();
            //Debuug.Log("colisiono con el player");
        }
    }

    void MePegan()
    {
        if (contadorVidas == 1)
        {
            shake.CamaraShake();

            GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);

            Destroy(gameObject);
        }

    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void OnTriggerExit(Collider colisionMouse)
    {
        if (colisionMouse.gameObject.tag == "Player")
        {
            Invoke("ResetMaterial", .2f);
        }
    }
}
