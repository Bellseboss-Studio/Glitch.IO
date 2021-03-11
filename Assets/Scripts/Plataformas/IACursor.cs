using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;
using UnityEngine.SceneManagement;

public class IACursor : MonoBehaviour {

    public Transform Player;
    public Transform Cursor;
    public GameObject[] zonasDePartida;
    public GameObject puntoActualdeZona;
    Vector2 dif;
    int numeroRandom;
    int index;

    public GameObject zonaParaBajar;
    [SerializeField] public bool deboCambiarDePosicion=true;

    public BoxCollider2D player;
    public int contadorVidas=0;

    private SpriteRenderer rendererFases;
    private Sprite fase0,fase1, fase2;
    public BoxCollider2D colliderCursor;

    public bool meHanPegado = false;
    private Shake shake;


    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    private UnityEngine.Object ReferenciaExplosion;

    private InstanciadorBoss instanciarBoss;

    public SpriteRenderer VidaPlayer, VidaEnemigo;
    public Sprite VidaEnemigoAlta, VidaEnemigoMedia, VidaEnemigoBaja;
    [SerializeField] private FinalizarStage finalizarstage;

    AudioSource cursorAudio;
    public AudioClip dañoPlayer;
    public AudioClip Muerte;

    public Animator programadorAnimacion;

    // Start is called before the first frame update
    void Start () {

        zonasDePartida = GameObject.FindGameObjectsWithTag ("zonaDePartida");
        numeroRandom = Random.Range (0, 30);

        index = Random.Range (0, zonasDePartida.Length);
        puntoActualdeZona = zonasDePartida[index];

        rendererFases = GetComponent<SpriteRenderer>();

        fase0 = Resources.Load<Sprite>("Cursor");
        fase1 = Resources.Load<Sprite>("Fase1");
        fase2 = Resources.Load<Sprite>("Fase2");

        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        ReferenciaExplosion = Resources.Load("CursorExplosion");

        instanciarBoss = GameObject.FindGameObjectWithTag("Cursor").GetComponent<InstanciadorBoss>();
        cursorAudio = GetComponent<AudioSource>();

        programadorAnimacion.SetFloat("programar", 0.2f);
        programadorAnimacion.SetFloat("golpear", 0.0f);
        programadorAnimacion.SetFloat("muerte", 0.0f);

    }

    private void FixedUpdate () {

        if (puntoActualdeZona != null)
        {

            dif = (puntoActualdeZona.transform.position - Cursor.transform.position);
            Cursor.GetComponent<Rigidbody2D>().velocity = dif;
        }

        if (deboCambiarDePosicion)
        {

            if (numeroRandom == Random.Range(0, 30)) {

                numeroRandom = Random.Range(0, 30);

                index = Random.Range(0, zonasDePartida.Length);
                puntoActualdeZona = zonasDePartida[index];

            }
           
        }

        else
        {
            bajarAqueLePeguen();
        }

    }
    void bajarAqueLePeguen()
    {
        puntoActualdeZona = zonaParaBajar;
        programadorAnimacion.SetFloat("programar", 0.0f);


    }
    async void MePegan()
    {

        if (contadorVidas==1)
        {
            meHanPegado = false;
            rendererFases.sprite = fase1;
            VidaEnemigo.sprite = VidaEnemigoMedia;
            shake.CamaraShake();
            colliderCursor.enabled = false;
        }

        if (contadorVidas == 2)
        {
            meHanPegado = false;
            rendererFases.sprite = fase2;
            VidaEnemigo.sprite = VidaEnemigoBaja;
            shake.CamaraShake();
            colliderCursor.enabled = false;
        }

        if (contadorVidas ==3)
        {
            programadorAnimacion.SetFloat("muerte", 0.2f);
            GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            finalizarstage.FinalizarEscenario("Has derrotado al cursor del programador", (int)EscenasParaCargar.NAVES_ESPACIALES);
            Destroy(GetComponent< InstanciadorBoss>());
            await EsperarQueSuba();
            Destroy(gameObject);
            //Aqui gana la escena, lo manda para las naves
        }

        if (meHanPegado == true)
        {
            colliderCursor.enabled = false;

            shake.CamaraShake();

            await EsperarQueSuba();



            //colliderCursor.enabled = true;

            /*if (contadorVidas < 3) {
                colliderCursor.enabled = true;
            }*/
        }

    }

    async private void OnCollisionEnter2D(Collision2D colisionMouse)
    {       
        if (colisionMouse.gameObject.tag == "Player")
        {
            cursorAudio.PlayOneShot(dañoPlayer);
            meHanPegado = true;
            contadorVidas++;

            programadorAnimacion.SetFloat("golpear", 0.2f);

            deboCambiarDePosicion = true;
            instanciarBoss.destruirCajasInstantaniamente();
            sr.material = matWhite;
            colliderCursor.enabled = false;
            MePegan();

            //Debuug.Log("colisiono con el player");
        }
        await EsperarQueSuba();
        programadorAnimacion.SetFloat("golpear", 0.0f);
        programadorAnimacion.SetFloat("programar", 0.2f);
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void OnCollisionExit2D(Collision2D colisionMouse)
    {
        if (colisionMouse.gameObject.tag == "Player")
        {
            Invoke("ResetMaterial", .2f);
        }
    }

    private async Task EsperarQueSuba()
    {
        await Task.Delay(TimeSpan.FromSeconds(1f));
    }
}