using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetosTrinidad : MonoBehaviour
{
    Rigidbody rb_objeto;
    public float speedObjeto = 0f;
    private UnityEngine.Object ReferenciaExplosion;
    Transform objeto_tr;
    ControladorDePlayerTrinidad playerController;

     Transform player;

    
    // Start is called before the first frame update
    void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb_objeto = GetComponent<Rigidbody>();
   
        //rb_objeto.velocity = new Vector3(0,0,0);
        rb_objeto.AddForce((player.position - this.transform.position)*speedObjeto);

        ReferenciaExplosion = Resources.Load("CursorExplosion");
        objeto_tr = GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorDePlayerTrinidad>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
       /*if (collision.gameObject.tag == "Player")
        {
            //playerController.vidas--;
            Destroy(gameObject);
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogWarning("objeto " + other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().enabled = false;
            Instantiate(ReferenciaExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
           
        }
        if (other.gameObject.tag == "ParedEliminadora")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.LogWarning("Enemigo");
            GetComponent<BoxCollider>().enabled = false;
            Instantiate(ReferenciaExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            other.GetComponent<ControladorDeVidaTrinidad>().vida--;
            other.GetComponent<ControladorDeVidaTrinidad>().SpritesDeVida();
        }
        if (other.gameObject.tag =="ParedRebotadora")
        {
            Debug.LogWarning("Muro");
            player = GameObject.FindGameObjectWithTag("Enemigo").transform;
            rb_objeto.AddForce((player.position - this.transform.position) * speedObjeto);
        }
    }
}
