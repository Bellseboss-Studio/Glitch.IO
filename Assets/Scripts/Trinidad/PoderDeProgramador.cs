using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;

public class PoderDeProgramador : ControladorDePoder
{
    public GameObject[] debugs;
    public Transform[] MM;
    bool objetoEnElCampo;
    public Animator anim_trini;
    int contador = 0;
    PoderDeDiseñador poderDeDiseñador;
    public Animator rayo;

    bool rayoActivado = false;
    public float tiempoaFalso = 2f;

    public anularAnimator anul_anim;
    public bool YourBool = true;
    public int tiempodeEspera = 3;

    private void Start()
    {
        poderDeDiseñador = GetComponent<PoderDeDiseñador>();
        anul_anim = GetComponent<anularAnimator>();

    }

public override bool ImplementacionDeLanzarPoder()
    {
        deltaTimeLocal += Time.deltaTime;
        bool respuesta = deltaTimeLocal >= tiempoDeLanzarPoder;
        if (respuesta) deltaTimeLocal = 0;
        return respuesta;
    }

    async protected override void Update()
    {

        //lanza objetos por las dos manos del medio.
        if (ImplementacionDeLanzarPoder())
        {
                GameObject Debugs = Instantiate(debugs[Random.Range(0, 2)]) as GameObject;
                Debugs.transform.position = MM[Random.Range(0, 2)].transform.position;

                if (Debugs.transform.position == MM[0].transform.position)
                {
                    anim_trini.SetFloat("ataqueDerecha", 0.0f);
                    anim_trini.SetFloat("ataqueIzquierda", 0.2f);
                }
                else
                {
                    anim_trini.SetFloat("ataqueDerecha", 0.2f);
                    anim_trini.SetFloat("ataqueIzquierda", 0.0f);
                }
            contador++;
            if(contador == 10)
            {
                anim_trini.SetFloat("ataqueDerecha", 0.0f);
                anim_trini.SetFloat("ataqueIzquierda", 0.0f);
                anim_trini.SetFloat("rayo", 0.2f);
                rayo.SetBool("lanzoRayo", true);
                rayoActivado = true;

                contador = 0;

            }
            if (contador == 2)
            {
                //Debuug.Log("entro a desactivar el rayo");
                deltaTimeLocal = 0;
                rayo.SetBool("lanzoRayo", false);
                anim_trini.SetFloat("rayo", 0.1f);
            }
            //lanzamos poder
            //Debuug.Log("Lanzamos el poder del " + this.name);
                await esperarQueCaigaBola();
                objetoEnElCampo = false;


        }
    }

    private async Task esperarQueCaigaBola()
    {

        await Task.Delay(TimeSpan.FromSeconds(0.1f));

    }
    
}
