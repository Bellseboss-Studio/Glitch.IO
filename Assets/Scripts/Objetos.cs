using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : MonoBehaviour
{
    Rigidbody rb_objeto;
    public float speedObjeto = 0f;
    private UnityEngine.Object ReferenciaExplosion;
    Transform objeto_tr;
    ThirdPersonCharacterControl playerController;

    public GameObject objeto;
    public VidaBoss vidaBoss;

    // Start is called before the first frame update
    void Start()
    {
        rb_objeto = GetComponent<Rigidbody>();

        //vidaBoss = GameObject.Find("musico_low").GetComponent<VidaBoss>();

        rb_objeto.velocity = new Vector3(0, 1, 0);
        rb_objeto.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
       
        ReferenciaExplosion = Resources.Load("CursorExplosion");
        objeto_tr = GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacterControl>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            playerController.vidas--;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DestruirVoladores")
        {
            Instantiate(ReferenciaExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
