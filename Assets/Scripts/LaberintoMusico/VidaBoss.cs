using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaBoss : MonoBehaviour
{
    public int vidaJefe;

    public Image VidaEnemigo;
    public Sprite VidaEnemigoAlta, VidaEnemigoMedia, VidaEnemigoBaja;

    public GameObject[] interruptores;
    GameObject instanciador;
    public InstanciarObjetosVoladores instanciaObjetosVoladores;
    Objetos objetos;
    public float velocidadObjetos;
    // Start is called before the first frame update
    void Start()
    {
        vidaJefe = 4;

    }

    public void cambiarSpriteVida()
    {

        if (vidaJefe == 3)
        {
            VidaEnemigo.sprite = VidaEnemigoAlta;

            if (instanciaObjetosVoladores.objetoEnElCampo == false)
            {         
                instanciaObjetosVoladores.SalirObjeto();
                velocidadObjetos = -15f;
                instanciaObjetosVoladores.objetoEnElCampo = true;
            }

        }
        if (vidaJefe == 2)
        {
            VidaEnemigo.sprite = VidaEnemigoMedia;
            if (instanciaObjetosVoladores.objetoEnElCampo == false)
            {
                instanciaObjetosVoladores.SalirObjeto();
                velocidadObjetos = -25f;
                instanciaObjetosVoladores.objetoEnElCampo = true;
            }
        }
        if (vidaJefe == 1)
        {
            VidaEnemigo.sprite = VidaEnemigoBaja;
            if (instanciaObjetosVoladores.objetoEnElCampo == false)
            {
                instanciaObjetosVoladores.SalirObjeto();
                velocidadObjetos = -35f;
                instanciaObjetosVoladores.objetoEnElCampo = true;
            }
        }
        if (vidaJefe == 0 && VidaEnemigo.sprite != null)
        {
            VidaEnemigo.sprite = null;
            finalizarStage.FinalizarEscenario("Le Dañamos todo al musico...", (int)EscenasParaCargar.TRINIDAD);
        }
    }
    [SerializeField] private FinalizarStage finalizarStage;
}
