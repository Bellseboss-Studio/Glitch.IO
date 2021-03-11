using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeVidaTrinidad : MonoBehaviour
{
    public int vida;
    public Sprite full, half, rip;
    public Image imaginador;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjetoLanzado"))
        {
            //Debuug.LogWarning("Triger " + other.gameObject.name);
            other.enabled = false;
        }
    }

    public void SpritesDeVida()
    {
        switch (vida)
        {
            case 2:
                imaginador.sprite = half;
                break;
            case 1:
                imaginador.sprite = rip;
                break;
            case 0:
                //Se murio
                gameObject.SetActive(false);
                finalizarStage.FinalizarEscenario("El bug gano! el juego no saldrá", (int)EscenasParaCargar.PRESENTACION);
                break;
        }
    }

    public FinalizarStage finalizarStage;
}
