using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeTrinidadTrinidad : MonoBehaviour
{
    public Transform player;
    public float speedDelay = 10;
    public Transform trinidad;

    public Image vidaTrinidad;
    public int vidaTrini = 3;
    public Animator anim_trini;

    private void Start()
    {
        anim_trini = GetComponent<Animator>();
    }
    void LateUpdate()
    {
        if (!player)
            return;

        var step = speedDelay * Time.deltaTime;

        // Rotate our transform a step closer to the target's.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, step);

        Quaternion neededRotation = Quaternion.LookRotation(player.transform.position - trinidad.transform.position);

        //use spherical interpollation over time 
        Quaternion interpolatedRotation = Quaternion.Slerp(trinidad.transform.rotation, neededRotation, Time.deltaTime * speedDelay);

    }

    Vector3 DirectionXZ()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Ignore Y
        return direction;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DañoCaja")
        {
            vidaTrini--;
            anim_trini.SetFloat("golpear", 0.2f);
        }
    }


}
