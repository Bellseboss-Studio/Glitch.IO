using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeAnimaciones : MonoBehaviour
{
    private int numberRandom;
    private float tiempoRandom;
    

    // Start is called before the first frame update
    void Start()
    {
        numberRandom = UnityEngine.Random.Range(0, 100);
        tiempoRandom = UnityEngine.Random.Range(0f, 2f);
        //////Debuug.LogWarning(tiempoRandom);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(numberRandom == UnityEngine.Random.Range(0, 100))
        {
            numberRandom = UnityEngine.Random.Range(0, 100);
            //lanzamos animacion de glich durante ese tiempo
            GetComponent<Animator>().SetBool("glich", true);
            ConteoDeGlich(tiempoRandom);
        }
    }

    private async void ConteoDeGlich(float tiempo)
    {
        await Task.Delay(TimeSpan.FromSeconds(tiempo));
        GetComponent<Animator>().SetBool("glich", false);
        tiempoRandom = UnityEngine.Random.Range(0f, 2f);
    }
}
