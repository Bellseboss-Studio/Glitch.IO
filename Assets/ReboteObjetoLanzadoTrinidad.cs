using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboteObjetoLanzadoTrinidad : MonoBehaviour
{
    public Transform trinidad;
    public float velocidadDevolucion=6;

    GameObject []objetosLanzados;
    public Transform [] posicionObjetosLanzados;

    Vector3 startPos;
    Transform dañoCaja;

    // Start is called before the first frame update
    void Start()
    {
        dañoCaja = GameObject.FindGameObjectWithTag("DañoCaja").GetComponent<Transform>();
        startPos = this.dañoCaja.position;


    }

    // Update is called once per frame
    void Update()
    {
        //float step = velocidadDevolución * Time.deltaTime;
        objetosLanzados = GameObject.FindGameObjectsWithTag("ObjetoLanzado");

        posicionObjetosLanzados = new Transform[objetosLanzados.Length];

        for (int i = 0; i < objetosLanzados.Length; i++)
        {
            posicionObjetosLanzados[i] = objetosLanzados[i].transform;
        }

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ObjetoLanzado")
        {
            //Debuug.Log("estoy rebotando");
            other.gameObject.GetComponent<Transform>().transform.position = dañoCaja.position;            //other.gameObject.GetComponent<Rigidbody>().AddForce((trinidad.position - this.transform.position) * velocidadDevolucion);
            dañoCaja.position = Vector3.MoveTowards(this.transform.position, startPos, Time.deltaTime);

            //Vector3.MoveTowards(this.transform.position, startPos, Time.deltaTime);
            //Vector3.MoveTowards(transform.position, target ? endPos.position : startPos, velocidadDevolucion * Time.deltaTime);


            //this.transform.position = Vector3.MoveTowards(transform.position, trinidad.position, velocidadDevolución * Time.deltaTime);
        }
    }
}
