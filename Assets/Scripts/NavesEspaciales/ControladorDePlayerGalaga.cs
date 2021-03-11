using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDePlayerGalaga : MonoBehaviour
{
    [SerializeField]private Shake shake;
    public float speed;
    public GameObject misil;
    public Transform zonaMisil;
    public Animator playerAnimator;
    public int cantidadMaximaDeBalasPorPantalla;
    public ComportamientoDeDesarrolladorGalaga desarrolladorComportamiento;
    public int vida=3;
    public BoxCollider2D nave;
    private GameObject player;
    [SerializeField] private GameOverScript gameOver;

    public SpriteRenderer VidaPlayer;
    public Sprite VidaAlta, VidaMedia, VidaBaja;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    private UnityEngine.Object ReferenciaExplosion;


    public AudioSource playerAudio;
    public AudioClip Muerte;
    public AudioClip Disparo;
    [SerializeField] private AudioClip golpe;

    bool estoyMuerto;

    private void Start()
    {
        estoyMuerto = false;
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        //vida = PlayerPrefs.GetInt("vida");
        player = GameObject.FindGameObjectWithTag("Player");
        vida = 3;

        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        ReferenciaExplosion = Resources.Load("CursorExplosion");
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Space) && estoyMuerto==false)
        {
            if(GameObject.FindGameObjectsWithTag("MisilPlayer").Length < cantidadMaximaDeBalasPorPantalla)
            {
                playerAudio.PlayOneShot(Disparo);
                //GetComponent<Animator>().SetTrigger("Disparar");
                //instanciamos el misil
                GameObject misilInstanciado = Instantiate(misil);
                misilInstanciado.transform.position = zonaMisil.transform.position;
                misilInstanciado.GetComponent<ControladorDeMovimientoDeMisil>().arriba = true;
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), misilInstanciado.GetComponent<BoxCollider2D>());
                //animator de disparo
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movimiento = new Vector2(x,y);
        GetComponent<Rigidbody2D>().velocity = movimiento * (speed * Time.deltaTime);

        if (y>=0.1f)
        {
            //GetComponent<Animator>().SetFloat("Subir",y);
        }
        if (y <= 0f)
        {
            //GetComponent<Animator>().SetFloat("Subir", y);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        shake.CamaraShake();
        Golpeo();
    }


    public void Golpeo()
    {
        AudioClip audioATocar = null;
        vida--;
        if (vida == 3)
        {
            VidaPlayer.sprite = VidaAlta;
        }
        if (vida == 2)
        {
            VidaPlayer.sprite = VidaMedia;
        }
        if (vida == 1)
        {
            VidaPlayer.sprite = VidaBaja;
        }
        audioATocar = golpe;
        //PlayerPrefs.SetInt("vida", vida);
        if (vida <= 0)
        {
            audioATocar = Muerte;
            estoyMuerto = true;
            nave.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            VidaPlayer.sprite = null;
            StartCoroutine(esperarQueAcabeAnimacion());

        }
        playerAudio.clip = audioATocar;
    }
    IEnumerator esperarQueAcabeAnimacion()
    {

        yield return new WaitForSeconds(1f);
        gameOver.GameOver("Nada se le escapa a este lapiz digital!",(int)EscenasParaCargar.NAVES_ESPACIALES);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemigo")
        {

            vida = 0;
            if (vida <= 0)
            {
                estoyMuerto = true;
                playerAudio.clip = Muerte;
                nave.enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
                VidaPlayer.sprite = null;
                StartCoroutine(esperarQueAcabeAnimacion());
            }

        }
    }


}
