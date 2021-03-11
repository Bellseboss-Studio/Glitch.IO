using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CAMBIOESCENA : MonoBehaviour 
{
    public VideoPlayer videocine;
    float deltaTimelocal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTimelocal += Time.deltaTime;
        if (deltaTimelocal > videocine.clip.length) {
            SceneManager.LoadScene((int)EscenasParaCargar.PLATAFORMA);
        }
    }
}
