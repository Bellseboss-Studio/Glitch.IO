using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDeMisil : MonoBehaviour
{
    [SerializeField] private Vector2 target, targetAux, posicionEnElMomento;
    [SerializeField] private GameObject posicionArriba, posicionAbajo;
    private bool arriba, llegamos;
    private Rigidbody2D rb;
    [SerializeField] private GameObject elQueRota;
    [SerializeField] private SpriteRenderer spriteDeMisil;
    public AudioSource audioMisilteledirigido;
    public AudioClip salida;
    public AudioClip explosion;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        audioMisilteledirigido = GetComponent<AudioSource>();

        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        arriba = Random.Range(0, 100) > 50;
        rb = GetComponent<Rigidbody2D>();

        posicionArriba = GameObject.Find("PosicionArriba");
        posicionAbajo = GameObject.Find("PosicionAbajo");

        targetAux = arriba ? posicionArriba.transform.position : posicionAbajo.transform.position;
        posicionEnElMomento = transform.position;

        audioMisilteledirigido.PlayOneShot(salida);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diff, diff2;
        diff = targetAux - posicionEnElMomento;
        diff2 = targetAux - (Vector2)transform.position;
        float anguloLocal = Vector2.SignedAngle(Vector2.up, diff2);
        Vector3 rotacion = elQueRota.transform.rotation.eulerAngles;
        rotacion.z = anguloLocal + 90;
        elQueRota.transform.eulerAngles = rotacion;
        if (!llegamos)
        {
            if(diff2.sqrMagnitude < 0.1f)
            {
                llegamos = true;
                targetAux = target;
                posicionEnElMomento = transform.position;
            }
        }

        rb.velocity = diff * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemigo") && !collision.gameObject.CompareTag("misilTeledirigido"))
        {
            audioMisilteledirigido.PlayOneShot(explosion);
            speed = 0;
            spriteDeMisil.enabled = false;
            GetComponent<Animator>().SetTrigger("morir");
        }
    }

    public void TerminarAnimacionDeMorirMisilTeledirigido()
    {
        Destroy(gameObject);
    }
}
