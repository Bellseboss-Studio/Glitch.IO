using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorMenuPausa : MonoBehaviour
{
    public bool estaPausado;

    public GameObject menuDePausa;
    public GameObject menuDeOpciones;

    ControladorMenuOpciones controladorMenuOpciones;

    public AudioSource audio;
    public Slider slider;

    private void Start()
    {

        controladorMenuOpciones = GetComponent<ControladorMenuOpciones>();
        controladorMenuOpciones.menuOpcionesActivo = true;

        if (PlayerPrefs.HasKey("volumenGeneral"))
        {
            slider.value = PlayerPrefs.GetFloat("volumenGeneral");
        }
        else
        {
            slider.value = 1;
        }
        audio.volume = slider.value;


    }
    private void Awake()
    {
        slider.onValueChanged.AddListener(delegate { CambioDeVolumenGeneral(); });
    }

    // Start is called before the first frame update
    void Update()
    {
        //si hacemos click en la tecla ESC nos saldrá el menú de escape
        if (Input.GetKeyDown(KeyCode.Escape) && controladorMenuOpciones.menuOpcionesActivo == true)
        {
            ponerPausaAlJuego();

        }
    }


    //Vamos a pausar el juego 
    public void ponerPausaAlJuego()
    {
       
        if (!estaPausado)
        {
            Time.timeScale = 0;
            menuDePausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menuDePausa.SetActive(false);
        }
        estaPausado = !estaPausado;


    }
    public void AbrirMenuOpciones()
    {
        menuDePausa.SetActive(false);
        menuDeOpciones.SetActive(menuDeOpciones);
        controladorMenuOpciones.menuOpcionesActivo = false;
    }
    public void quitarPausaAlJuego()
    {
        Time.timeScale = 1;
        menuDePausa.SetActive(false);

    }
    public void VolverAlMenuPrincipal()
    {
        quitarPausaAlJuego();
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void CambioDeVolumenGeneral()
    {
        audio.volume = slider.value;
        PlayerPrefs.SetFloat("volumenGeneral", slider.value);
    }
}