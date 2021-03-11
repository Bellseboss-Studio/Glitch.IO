using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    //salto controlado
    public bool comenzarContar, logeoDeSalto;
    private Rigidbody2D rb;
    private float min, max, x, y, deltaTimeLocal, alturamax, deltaTimeLocalParaControl, velocidadDash, deltaTimeLocalParaDashHaciaAtras;
    //hasta aqui
    [SerializeField] private Sprite aBueno, aMalo, dBueno, dMalo, spaceBueno, spaceMalo;
    [SerializeField] private Image Ia, Id, Ispace;
    [SerializeField] private int rangoEnX, rangoEnY;

    public float speed = 100.0f;
    public float jumpForce = 350.0f;
    public float airDrag = 0.8f;
    public Transform bottomTransform;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 currentVelocity;
    private float previousPositionY;
    private Shake shake;

    public bool isOnGround = true;
    public bool isJumping;
    public bool puedeSaltar = true;
    public bool puedeIrHaciaDerecha = true;
    public bool puedeIrHaciaIzquierda = true;
    public int vidas = 2;

    public SpriteRenderer VidaPersonaje;
    public Sprite VidaMedia, VidaBaja;
    private UnityEngine.Object ReferenciaExplosion;
    public float cantidadDeDeltatime;
    [SerializeField] private GameOverScript gameOver;

    AudioSource glitchAudio;
    public AudioClip glitch;
    public AudioClip saltarGlitch;
    public AudioClip dañoGlitch;


    // Start is called before the first frame update
    void Start()
    {
        glitchAudio = GetComponent<AudioSource>();
        //salto controlado
        min = -1f;
        max = 1f;
        deltaTimeLocal = min;
        rb = GetComponent<Rigidbody2D>();
        velocidadDash = 1;
        //hasta aqui

        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CambiarColorDeLasTeclas();

        ReferenciaExplosion = Resources.Load("CursorExplosion");

    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        Move();
        previousPositionY = transform.position.y;
        Saltar();
        //controlador de los sprites
        if (!puedeIrHaciaDerecha)
        {
            Id.sprite = dMalo;
        }
        else
        {
            Id.sprite = dBueno;
        }

        if (!puedeIrHaciaIzquierda)
        {
            Ia.sprite = aMalo;
        }
        else
        {
            Ia.sprite = aBueno;
        }
        if (!puedeSaltar)
        {
            Ispace.sprite = spaceMalo;
        }
        else
        {
            Ispace.sprite = spaceBueno;
        }
    }

    private void Move()
    {
        //float velocity = Input.GetAxis("Horizontal") * speed;

        isJumping = Input.GetKey(KeyCode.Space);

        //animator.SetFloat("Speed", Mathf.Abs(velocity));

        /*if (!isOnGround)
        {
            velocity *= airDrag;
        }*/

        // Horizontal Movement
        //body.velocity = Vector2.SmoothDamp(body.velocity, new Vector2(velocity, body.velocity.y), ref currentVelocity, 0.02f);



        // Initiate Jump
        if (isJumping)
        {
            //body.AddForce(new Vector2(0, jumpForce));
            if (isOnGround)
            {
                glitchAudio.clip = saltarGlitch;
                glitchAudio.Play();
                comenzarContar = true;
            }
            //animator.SetBool("IsJumping", true);
            isOnGround = false;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CajaInstanciada"))
        {

            shake.CamaraShake();
            this.vidas--;
            glitchAudio.PlayOneShot(dañoGlitch);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ResetObject();
        ////Debuug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("CajaInstanciada"))
        {
            //mandamos a volar la caja
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(rangoEnX * -1, rangoEnX), Random.Range(rangoEnY, rangoEnY * 2)));
            ////Debuug.Log(vidas);
            if (vidas == 1)
            {
                VidaPersonaje.sprite = VidaMedia;
            }
            if (vidas == 0)
            {
                VidaPersonaje.sprite = VidaBaja;
            }
            if (vidas < 0)
            {
                //ya murio
                GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);

                Destroy(gameObject);
                VidaPersonaje.sprite = null;
                EsperarQueJugadorSeDesintegre();

                gameOver.GameOver("Con un par de debugs pude eliminarte, no fuiste reto!",(int)EscenasParaCargar.PLATAFORMA);
            }

            CambiarColorDeLasTeclas();
        }
        if (!collision.gameObject.CompareTag("Respawn"))
        {
            isOnGround = true;
        }

    }

    private void Saltar()
    {
        if (comenzarContar)
        {
            if (!puedeSaltar)
            {
                ResetObject();
                return;
            }
            else
            {
                Ispace.sprite = spaceBueno;

            }

            deltaTimeLocal += (Time.deltaTime * cantidadDeDeltatime);
            //ir modificando a una funcion matematica mas suave en su movimiento
            y = Mathf.Cos(deltaTimeLocal) * jumpForce;
            //Como por ejemplo
            //2x^(3)+2
            //y = ((2 * Mathf.Pow(deltaTimeLocal, 3)) + 2) * speedJump;
            if (y > alturamax)
            {
                alturamax = y;
            }
            else
            {
                //y *= -1;
                deltaTimeLocal += max;
            }
            if (deltaTimeLocal >= max)
            {
                ResetObject();
            }
        }
        else
        {
            y = rb.velocity.y;
        }

        float x = Input.GetAxis("Horizontal");
        if (!puedeIrHaciaDerecha)
        {
            if(x > 0)
            {
                x = 0;
            }
        }
        if (!puedeIrHaciaIzquierda)
        {
            if (x < 0)
            {
                x = 0;
            }
        }
        rb.velocity = new Vector2( x * speed * velocidadDash, y);
        if (x < 0) spriteRenderer.flipX = true;
        else if (x > 0) spriteRenderer.flipX = false;
    }

    public void ResetObject()
    {
        deltaTimeLocal = min;
        y = 0;
        alturamax = -1;
        comenzarContar = false;
    }

    private void CambiarColorDeLasTeclas()
    {
        Color myColor = new Color();
        switch (vidas)
        {
            case 2:
                //verde
                ColorUtility.TryParseHtmlString("#4ed302", out myColor);
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#d3a902", out myColor);
                break;
            case 0:
                ColorUtility.TryParseHtmlString("#d30202", out myColor);
                break;
        }
        Id.color = Ia.color = Ispace.color = myColor;
        
    }
    private async Task EsperarQueJugadorSeDesintegre()
    {

        await Task.Delay(TimeSpan.FromSeconds(2f));

    }
}