using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMenuOpciones : MonoBehaviour
{
    public GameObject menuDeOpciones;
    public GameObject menuDePausa;
    [SerializeField] public bool menuOpcionesActivo;
    [SerializeField] ControladorMenuPausa controladorMenuPausa;
    //[SerializeField] private Image imagenBrillo;
    //public Slider brilloEscena;

    public float rbgValue = 0.5f;

    private void Start()
    {
        controladorMenuPausa = GetComponent<ControladorMenuPausa>();


        //imagenBrillo = GameObject.Find("PaneldeBrillo").GetComponent<Image>();

        /*brilloEscena.onValueChanged.AddListener(delegate
        {
            CogerBrillo();
        });*/

        //Guardamos los values del jugador en la opacidad
        /*if (PlayerPrefs.HasKey("opacidad"))
        {
            Color opacidad = imagenBrillo.color;
            opacidad.a = PlayerPrefs.GetFloat("opacidad");
            //opacidad.a = brilloEscena.value;

            imagenBrillo.color = opacidad;
            brilloEscena.value = opacidad.a;

        }*/

        //CogerBrillo();
    }

    public void VolverAlMenuPrincipal()
    {
        controladorMenuPausa.estaPausado = true;
        menuOpcionesActivo = true;
        menuDeOpciones.SetActive(false);
        menuDePausa.SetActive(true);
    }
    //HacerBrillo
    /*public void CogerBrillo()
    {
        Color opacidad = imagenBrillo.color;
        opacidad.a = (brilloEscena.value * 80) / 100;
        //opacidad.a = brilloEscena.value;
        imagenBrillo.color = opacidad;
        PlayerPrefs.SetFloat("opacidad", opacidad.a);
    }*/
}
