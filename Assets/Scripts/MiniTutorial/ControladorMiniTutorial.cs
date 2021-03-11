using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;
using UnityEngine.SceneManagement;

public class ControladorMiniTutorial : MonoBehaviour
{
    GameObject cajaDaño;
    public GameObject textoDaño;
    public Rigidbody2D cajaTutorial;
    public GameObject enemigoEasy;
    public Text texto;

    bool mFaded = false;
    public float duracionFadeTexto = 0.4f;
    ControladorCanvas controladorCanvas;

    // Start is called before the first frame update
    async void Start()
    {
        cajaDaño = GameObject.FindGameObjectWithTag("CajaTutorial");
        cajaTutorial.gravityScale = 0;

        await EsperarAqueJuegueUnPoco();
        bajarCaja();

        await EsperarTexto();
        aparecerEnemigo();

        controladorCanvas = GetComponent<ControladorCanvas>();

    }

    // Update is called once per frame
    async void Update()
    {
        if (cajaDaño==null)
        {
            textoDaño.SetActive(true);

        }
        if(enemigoEasy == null)
        {

            texto.text = "Perfecto, has derrotado al enemigo, ahora ya puedes ir hacia tu primer duelo, mucha suerte.";
            await EsperarTexto();
            texto.text = "";

        }
    }



    void bajarCaja()
    {
        cajaTutorial.gravityScale = 1;
    }
    async void aparecerEnemigo()
    {
        enemigoEasy.SetActive(true);
        await EsperarTexto();


        texto.text = "Ha aparecido un enemigo, prueba golpearle con tu propio cuerpo para derrotarlo.";
        await EsperarTexto();

    }

    private async Task EsperarAqueJuegueUnPoco()
    {

        await Task.Delay(TimeSpan.FromSeconds(6f));

    }
    private async Task EsperarTexto()
    {

        await Task.Delay(TimeSpan.FromSeconds(3f));

    }
}
