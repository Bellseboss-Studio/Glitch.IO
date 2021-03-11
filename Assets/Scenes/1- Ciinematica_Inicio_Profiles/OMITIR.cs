using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OMITIR : MonoBehaviour {
    public static Animator anim;


    void Awake()
    {

        anim = GetComponent<Animator>();

    }
    void Start()
    {


        anim = GetComponent<Animator>();


    }


  
	
	// Update is called once per frame
	public void omitir()
    {
        SceneManager.LoadScene((int)EscenasParaCargar.PLATAFORMA);

    }
}
