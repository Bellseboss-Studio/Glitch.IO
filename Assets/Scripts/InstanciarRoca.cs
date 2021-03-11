using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;

public class InstanciarRoca : MonoBehaviour
{
    public GameObject bola;
    public Transform salidaDeBola;



    bool bolaEnElCampo;

    void Update()
    {
        if (bolaEnElCampo==false)
        {
            bolaEnElCampo = true;
            SalirBola();
        }

    }

    async void SalirBola()
    {
        GameObject bolaInstanciada = Instantiate(bola);
        bolaInstanciada.transform.position = salidaDeBola.transform.position;

        await esperarQueCaigaBola();
        bolaEnElCampo = false;
    }

    private async Task esperarQueCaigaBola()
    {

        await Task.Delay(TimeSpan.FromSeconds(2f));

    }
}
