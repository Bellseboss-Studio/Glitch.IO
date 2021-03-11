using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ControladorDeVIdaDelDesarrollador : MonoBehaviour
{
    [SerializeField] public int vida;
    private Shake shake;

    public AudioSource naveEnemigaAudio;
    public AudioClip danioNave;

    private Material matWhite;
    private Material matDefault;
    private Color colorBlanco = Color.white;
    private Color colorRed = Color.red;
    SpriteRenderer sr;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        ControladorDeVida();

        sr = GetComponent<SpriteRenderer>();
        sr.color = colorBlanco;
        //matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        //matDefault = sr.material;
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MisilPlayer")
        {
            //sr.material = matWhite;
            sr.color = colorRed;
            naveEnemigaAudio.PlayOneShot(danioNave);
            shake.CamaraShake();
            GetComponent<PlayableDirector>().Stop();
            GetComponent<ComportamientoDeDesarrolladorGalaga>().moverLugarSeguro = true;
            vida--;

            ControladorDeVida();
            Invoke("ResetMaterial", .2f);
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("MisilPlayer"))
            {
                Destroy(o);
            }
        }
    }

    public void ControladorDeVida()
    {

        Debug.Log("entro al controlado de la vida del malooooooo uengu"+vida);
        if (vida <= 16)
        {
            Debug.Log("entro al de 16!!!!!"+vida);
            GetComponent<ComportamientoDeDesarrolladorGalaga>().VidaTress();
        }
        if (vida <= 12)
        {
            Debug.Log("entro al de 12!!!!!");
            GetComponent<ComportamientoDeDesarrolladorGalaga>().VidaDoss();
        }
        if (vida <= 9)
        {
            Debug.Log("entro al de 19!!!!!");
            GetComponent<ComportamientoDeDesarrolladorGalaga>().VidaUno();
        }
        if (vida <= 0)
        {
            Debug.Log("entro al de muereeee!!!!");
            GetComponent<ComportamientoDeDesarrolladorGalaga>().SeMurio();
        }
    }

    void ResetMaterial()
    {
        sr.color = colorBlanco;
    }

}
