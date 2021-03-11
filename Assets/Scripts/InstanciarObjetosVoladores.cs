using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;

public class InstanciarObjetosVoladores : MonoBehaviour
{
    public GameObject[] Objeto;
    public Transform [] salidaObjetos;

    public bool objetoEnElCampo;

    // Start is called before the first frame update
    public async void SalirObjeto()
    {
        GameObject bolaInstanciada = Instantiate(Objeto[Random.Range(1,4)]) as GameObject;
        bolaInstanciada.transform.position = salidaObjetos[Random.Range(1, 13)].transform.position;

        await esperarQueCaigaBola();
        objetoEnElCampo = false;
    }

    private async Task esperarQueCaigaBola()
    {

        await Task.Delay(TimeSpan.FromSeconds(0.1f));

    }

}
