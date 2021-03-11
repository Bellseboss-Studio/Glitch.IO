using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarPlayer : MonoBehaviour
{
    CharacterController2D characterController2D;
    private Shake shake;
     


    // Start is called before the first frame update
    void Start()
    {
        characterController2D = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "CajaTutorial")
        {

            characterController2D.vidas--;
        }

        if (collision.gameObject.tag == "CajaInstanciada")
        {
            //shake.CamaraShake();
           //characterController2D.vidas--;
            
            
        }

    }
}
