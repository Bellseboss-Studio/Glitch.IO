using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ControladorDeVsDeNaves : MonoBehaviour
{
    [SerializeField] private List<GameObject> objetosParaActivar;
    [SerializeField] private PlayableDirector director;
    private float deltaTimeLocal;
    private void Start()
    {
        /*if (PlayerPrefs.HasKey("vida"))
        {
            if(PlayerPrefs.GetInt("vida") <= 0)
            {
                SceneManager.LoadScene("Plataformas");
            }
        }
        else
        {
            PlayerPrefs.SetInt("vida", 1);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        deltaTimeLocal += Time.deltaTime;
        if(deltaTimeLocal >= director.duration + 3)
        {
            foreach(GameObject o in objetosParaActivar)
            {
                o.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
