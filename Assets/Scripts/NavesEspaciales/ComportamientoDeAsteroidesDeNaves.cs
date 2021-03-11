using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDeAsteroidesDeNaves : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.left * (velocidad * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemigo"))
        {
            velocidad = 0;
            GetComponent<Animator>().SetTrigger("morir");
        }
    }

    public void TerminarDeMorir()
    {
        Destroy(gameObject);
    }
}
