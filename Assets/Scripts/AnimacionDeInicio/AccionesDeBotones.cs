using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class AccionesDeBotones : MonoBehaviour
{
    public PlayableDirector director;
    public PlayableAsset anim;

    private void Start()
    {
        director.Stop();
    }

    public void BotonParaCrear()
    {
       
        director.playableAsset = Instantiate(anim);

        director.Play();

        //SceneManager.LoadScene((int)EscenasParaCargar.PLATAFORMA);
    }
    public void BotonParaSalir()
    {
        Application.Quit();

    }
    public void BotonParaCreditos()
    {
        //SceneManager.LoadScene((int)EscenasParaCargar.PLATAFORMA);
    }
}
