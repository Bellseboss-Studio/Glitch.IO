using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;
using UnityEngine.UI;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    InterruptoresMecanica interruptoresMecanicas;

    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    public int vidas = 2;
    public Image VidaPersonaje;
    public Sprite VidaMedia, VidaBaja;
    private UnityEngine.Object ReferenciaExplosion;
    public CinemachineShake shakeCinemachine;
    Vector3 moveVector;

    AudioSource playerAudio;
    public AudioClip Muerte;

    private void Start()
    {
        ReferenciaExplosion = Resources.Load("CursorExplosion");
        playerAudio = GetComponent<AudioSource>();
    }

    async private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >=0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f)*Vector3.forward;

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        moveVector = Vector3.zero;

        //Check if cjharacter is grounded
        if (controller.isGrounded == false)
        {
            //Add our gravity Vecotr
            moveVector += Physics.gravity;
        }

        //Apply our move Vector , remeber to multiply by Time.delta
        controller.Move(moveVector * Time.deltaTime);

        if (vidas == 1)
        {
            shakeCinemachine.DoShake();
            VidaPersonaje.sprite = VidaMedia;
        }
        if (vidas == 0)
        {
            shakeCinemachine.DoShake();
            VidaPersonaje.sprite = VidaBaja;
        }
        if (vidas < 0 && VidaPersonaje.sprite != null)
        {
            shakeCinemachine.DoShake();
            //ya murio
            GameObject explosion = (GameObject)Instantiate(ReferenciaExplosion);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            playerAudio.clip = Muerte;
            Destroy(gameObject);
            VidaPersonaje.sprite = null;
            gameOver.GameOver("Game Over",(int)EscenasParaCargar.LABERINTO);
            await EsperarQueJugadorSeDesintegre();
        }
    }

    [SerializeField] private GameOverScript gameOver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="MartilloPilon")
        {
            vidas=-3;

        }
    }

    private async Task EsperarQueJugadorSeDesintegre()
    {

        await Task.Delay(TimeSpan.FromSeconds(2f));

    }

}