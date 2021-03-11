using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocidadBola : MonoBehaviour
{
    Rigidbody rb_bola;
    public float speedBola = 5f;
    private UnityEngine.Object ReferenciaExplosion;
    Transform bola_tr;

    ThirdPersonCharacterControl playerController;

    AudioSource playerAudio;
    public AudioClip dañoPlayer;

    AudioSource bolasAudio;
    public AudioClip borrarBola;

    // Start is called before the first frame update
    void Start()
    {
        rb_bola = GetComponent<Rigidbody>();

        rb_bola.velocity = new Vector3(speedBola, 0, 0);
        rb_bola.AddForce(new Vector3(speedBola, 0, 0), ForceMode.Impulse);

        ReferenciaExplosion = Resources.Load("BolaExplosion");
        bola_tr = GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacterControl>();

        playerAudio = GetComponent<AudioSource>();
        bolasAudio= GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DestruirObjetos")
        {
            bolasAudio.clip = borrarBola;
            Instantiate(ReferenciaExplosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            playerAudio.clip = dañoPlayer;
            playerController.vidas--;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "MartilloPilon")
        {
            bolasAudio.clip = borrarBola;
            Instantiate(ReferenciaExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
