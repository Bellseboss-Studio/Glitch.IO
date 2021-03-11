using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoderDeMusico : ControladorDePoder
{
    GameObject ParedGanadora;

    public float tiempoEntreParedes;
    public GameObject[] Muros;

    bool bajo;
    int transitionSpeed = 5;

    int numeroRandom;

    private void Start()
    {
        Muros = GameObject.FindGameObjectsWithTag("Muros");
    }
    public override bool ImplementacionDeLanzarPoder()
    {
        deltaTimeLocal += Time.deltaTime;
        bool respuesta = deltaTimeLocal >= tiempoDeLanzarPoder;
        if (respuesta) deltaTimeLocal = 0;
        return respuesta;
    }

    protected override void Update()
    {
        deltaTimeLocal += Time.deltaTime;

        if (deltaTimeLocal >= tiempoEntreParedes)
        {
            deltaTimeLocal = 0;

            if (bajo)
            {
                bajo = false;
                StartCoroutine(animacionDeSubidaDeMuro());
            }
            else
            {
                BajarParedes();
                bajo = true;
            }
        }

        if (ImplementacionDeLanzarPoder())
        {

            //lanzamos poder
            //Debuug.Log("Lanzamos el poder del " + this.name);
        }
    }
    void BajarParedes()
    {
        ParedGanadora = Muros[Random.Range(0, 33)];
        StartCoroutine(animacionDeBajadaDeMuro());

    }
    IEnumerator animacionDeBajadaDeMuro()
    {

        for (int i = 0; i <= 10; i++)
        {

            ParedGanadora.transform.position = new Vector3(ParedGanadora.transform.position.x, ParedGanadora.transform.position.y + (10f / 11), ParedGanadora.transform.position.z);
            yield return new WaitForSeconds(tiempoDeSubida);
        }

    }
    public float tiempoDeSubida;

    IEnumerator animacionDeSubidaDeMuro()
    {
        for (int i = 0; i <= 10; i++)
        {

            ParedGanadora.transform.position = new Vector3(ParedGanadora.transform.position.x, ParedGanadora.transform.position.y - (10f / 11), ParedGanadora.transform.position.z);
            //Vector3.Lerp(posicionInicialMuros6, murosLerpFinal, transitionSpeed);
            yield return new WaitForSeconds(tiempoDeSubida);
        }
    }

}
