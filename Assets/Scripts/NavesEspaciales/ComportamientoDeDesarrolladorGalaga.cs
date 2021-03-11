using UnityEngine;
using UnityEngine.Playables;
using System.Threading.Tasks;
//using System;
using System.Collections;


public class ComportamientoDeDesarrolladorGalaga : MonoBehaviour
{
    public GameObject misil, misilTeledirigido;
    public Transform zonaDeBala;
    public Transform zonaDeBala2;
    [SerializeField] private PlayableAsset fase1, fase2, fase3, fase3Alt;
    PlayableAsset nuevaFase = null;
    public SpriteRenderer spr_Pinturillo;
    public Sprite pinturillo, pinturilloPlus, behemot;
    private Object ReferenciaExplosion;
    public SpriteRenderer VidaEnemigo;
    public Sprite VidaAlta, VidaMedia, VidaBaja;
    public GameObject primeraFase, segundaFase, terceraFase;

    AudioSource naveEnemigaAudio;
    public AudioClip muerteNave;

    public Animator artistaAnim;
    [SerializeField] private FinalizarStage finStage;
    private float deltaTimeLocal;
    public bool moverLugarSeguro;
    [SerializeField] private GameObject lugarSeguro;
    private bool disparamosRayoLacer;
    private bool yaPasoPorAqui;
    private float tiempoDeFase;




    // Start is called before the first frame update
    private void Awake()
    {
        ReferenciaExplosion = Resources.Load("CursorExplosion");
        naveEnemigaAudio = GetComponent<AudioSource>();
        segundaFase.SetActive(false);
        terceraFase.SetActive(false);
        primeraFase.SetActive(false);
        nuevaFase = fase1;
        DirectorDeMovimientos();

    }

    
    public void DirectorDeMovimientos()
    {
        GetComponent<PlayableDirector>().Stop();
        GetComponent<PlayableDirector>().playableAsset = Instantiate(nuevaFase);
        nuevaFase = GetComponent<PlayableDirector>().playableAsset;
        GetComponent<PlayableDirector>().Play();
        tiempoDeFase = (float)nuevaFase.duration;
    }

    public void SeMurio()
    {
        artistaAnim.SetTrigger("muerte");
        naveEnemigaAudio.PlayOneShot(muerteNave);
        GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        VidaEnemigo.sprite = null;
        Destroy(gameObject);
        //llamamos al finalizar para fonalizar
        finStage.FinalizarEscenario("Al grafista se le cayo el lapiz digital", (int)EscenasParaCargar.LABERINTO);
    }

    public void VidaUno()
    {
        spr_Pinturillo.sprite = behemot;
        VidaEnemigo.sprite = VidaBaja;
        nuevaFase = fase3;
        artistaAnim.SetTrigger("golpear");
        segundaFase.SetActive(false);
        terceraFase.SetActive(false);
        primeraFase.SetActive(true);
    }

    public void VidaDoss()
    {
        spr_Pinturillo.sprite = pinturilloPlus;
        VidaEnemigo.sprite = VidaMedia;
        nuevaFase = fase2;
        artistaAnim.SetTrigger("golpear");
        segundaFase.SetActive(true);
        terceraFase.SetActive(false);
        primeraFase.SetActive(false);
    }

    public void VidaTress()
    {
        spr_Pinturillo.sprite = pinturillo;
        VidaEnemigo.sprite = VidaAlta;
        nuevaFase = fase1;
        segundaFase.SetActive(false);
        terceraFase.SetActive(true);
        primeraFase.SetActive(false);
    }

    public void RegresarNormalmente()
    {
        disparamosRayoLacer = false;
        moverLugarSeguro = false;
        yaPasoPorAqui = false;
    }

    // debe mandar un misil de vez en cuando
    void FixedUpdate()
    {
        Disparar();
        if (moverLugarSeguro)
        {
            GetComponent<Rigidbody2D>().velocity = (lugarSeguro.transform.position - transform.position) * 2;
            Vector2 magnitudDeZonas = GetComponent<Rigidbody2D>().velocity;
            if (magnitudDeZonas.magnitude < 0.1)
            {
                if (disparamosRayoLacer)
                {
                    if (!yaPasoPorAqui)
                    {
                        GetComponent<Animator>().SetTrigger("rayoLaser");
                        yaPasoPorAqui = true;
                    }
                    return;
                }
                moverLugarSeguro = false;
                DirectorDeMovimientos();
            }
            return;
        }
        deltaTimeLocal += Time.deltaTime;
        if (deltaTimeLocal >= (tiempoDeFase))
        {
            deltaTimeLocal = 0;
            if (GameObject.Find("Player") != null)
            {
                QueDebeHacerElDesarrollador(GetComponent<ControladorDeVIdaDelDesarrollador>().vida);
            }
            else
            {
                return;
            }
        }

    }

    private void Disparar()
    {
        if (Random.Range(0, 100) == Random.Range(0, 100))
        {
            GameObject misilInstanciado = Instantiate(misil);

            misilInstanciado.transform.position = zonaDeBala.position;

            GameObject misilInstanciado2 = Instantiate(misil);

            misilInstanciado2.transform.position = zonaDeBala2.position;
        }
    }

    private void QueDebeHacerElDesarrollador(int vidaLocal)
    {
        //vamos a mirar por faces, con un stwich
        if (vidaLocal <= 12)
        {
            if (GameObject.FindGameObjectsWithTag("misilTeledirigido").Length <= 2)
            {
                GameObject misil = Instantiate(misilTeledirigido);
                misil.transform.position = transform.position;
            }
        }
        if (vidaLocal <= 9)
        {
            if (Random.Range(0, 100) > 60)
            {
                //vamos a ponernos para disparar el rayo lacer
                GetComponent<PlayableDirector>().Stop();
                moverLugarSeguro = true;
                disparamosRayoLacer = true;
            }
        }
    }
}
