using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeMovimientoDeMisil : MonoBehaviour
{
    public float speed, aumento;
    public bool arriba;
    public float eulerAngle;

    // Start is called before the first frame update
    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        //playerAudio.clip = Disparo;

        if (!arriba)
        {
            Quaternion rotacion = gameObject.transform.rotation;
            rotacion.eulerAngles = new Vector3(rotacion.eulerAngles.x, rotacion.eulerAngles.y, 180);
            gameObject.transform.rotation = rotacion;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = (arriba?Vector2.right:Vector2.left) * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MisilEnemigo") || collision.gameObject.CompareTag("Enemigo") || collision.gameObject.CompareTag("DesaparecerMisil"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetTrigger("choco");
            FinDelChoque();
        }
    }

    public void InicioDeChique()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void FinDelChoque()
    {
        Destroy(gameObject);
    }
}
