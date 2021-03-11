using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anularAnimator : MonoBehaviour
{
    public PoderDeProgramador poder_programador;

    public void Start()
    {
        poder_programador.rayo = GetComponent<Animator>();
    }

}
