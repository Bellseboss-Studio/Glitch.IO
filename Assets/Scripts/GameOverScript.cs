using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TextMeshProUGUI texto;
    //[SerializeField] private 
    private int saltoDeEscena;

    public void GameOver(string tex, int escenaQueDebeSaltar)
    {
        texto.text = tex;
        saltoDeEscena = escenaQueDebeSaltar;
        director.Play();
        StartCoroutine(EsperarQueTermineElDirector());
    }

    IEnumerator EsperarQueTermineElDirector()
    {
        yield return new WaitForSeconds((float)director.duration + 5);
        SceneManager.LoadScene(saltoDeEscena);
    }
}
