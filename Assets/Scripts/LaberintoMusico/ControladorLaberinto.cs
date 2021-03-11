using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;
using UnityEngine.SceneManagement;


public class ControladorLaberinto : MonoBehaviour
{
    public GameObject[] Muros;

    bool bajo;
    int transitionSpeed = 5;

    int numeroRandom;

    public void Start()
    {
        Muros = GameObject.FindGameObjectsWithTag("Paredes");

    }

    float deltaTimeLocal=0f;

    public float tiempoEntreParedes;

    void Update()
    {
        deltaTimeLocal += Time.deltaTime;

        if (deltaTimeLocal >= tiempoEntreParedes)
        {
            deltaTimeLocal = 0;

            if (bajo){
                bajo = false;
                StartCoroutine(animacionDeSubidaDeMuro());
            }
            else
            {
                BajarParedes();
                bajo = true;
            }
        }
    }
    GameObject ParedGanadora;

    void BajarParedes()
    {
        ParedGanadora = Muros[Random.Range(0, 6)];
        StartCoroutine(animacionDeBajadaDeMuro());

    }

    IEnumerator animacionDeBajadaDeMuro()
    {

        for (int i = 0; i <= 10; i++ )
        {

            ParedGanadora.transform.position = new Vector3(ParedGanadora.transform.position.x, ParedGanadora.transform.position.y - (1.38f/11), ParedGanadora.transform.position.z);
                yield return new WaitForSeconds(tiempoDeSubida);
        }
        
    }
    public float tiempoDeSubida;

    IEnumerator animacionDeSubidaDeMuro()
    {
        for (int i = 0; i <= 10; i++)
        {

            ParedGanadora.transform.position = new Vector3(ParedGanadora.transform.position.x, ParedGanadora.transform.position.y + (1.38f / 11), ParedGanadora.transform.position.z);
            //Vector3.Lerp(posicionInicialMuros6, murosLerpFinal, transitionSpeed);
            yield return new WaitForSeconds(tiempoDeSubida);
        }
    }
}
