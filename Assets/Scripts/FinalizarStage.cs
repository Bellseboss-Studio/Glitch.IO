using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalizarStage : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    private int saltoDeEscena;
    [SerializeField] private TextMeshProUGUI texto;

    public void FinalizarEscenario(string textoMostrar, int escenaSaltar)
    {
        texto.text = textoMostrar;
        saltoDeEscena = escenaSaltar;
        director.Play();
        StartCoroutine(EsperarQueSeAcabeLaAnimacion());
    }

    IEnumerator EsperarQueSeAcabeLaAnimacion()
    {
        yield return new WaitForSeconds((float)director.duration + 10);
        SceneManager.LoadScene(saltoDeEscena);
    }
}
