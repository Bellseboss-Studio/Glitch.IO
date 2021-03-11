using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDePlayerTrinidad : MonoBehaviour
{
    private bool comenzarContar;
    private bool puedeSaltar = true;
    private float deltaTimeLocal;
    private float alturamax;
    private bool isOnGround;
    private float y;
    [SerializeField] private float jumpForce;
    Vector3 jump;
    [SerializeField] private float max;
    private Rigidbody rb;

    private float x;
    [SerializeField] private float min;
    [SerializeField] private GameObject referenciaQuienRota;
    [SerializeField] private float velicidadDeAltura;
    bool isGrounded;
    public float gravedad = 10f;
    public int vida;
    public Sprite full, half, rip;
    public Image imaginador;
    public GameObject trinidad;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        //salto controlado
        deltaTimeLocal = min;
        //hasta aqui
    }
    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravedad, ForceMode.Acceleration);
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        float anguloLocal = Input.GetAxis("Horizontal") * -1;
        Vector3 rotacion = referenciaQuienRota.transform.rotation.eulerAngles;
        rotacion.y += anguloLocal;
        referenciaQuienRota.transform.eulerAngles = rotacion;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rayo") || other.gameObject.CompareTag("ObjetoLanzado"))
        {
            //Debuug.LogWarning("Triger " + other.gameObject.name);
            other.enabled = false;
            vida--;
            SpritesDeVida();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn")) 
        {
            trinidad.SetActive(false);
            gameOver.GameOver("Te caiste en el compilador! toca reiniciar", (int)EscenasParaCargar.TRINIDAD);
        }
    }

    public void SpritesDeVida()
    {
        switch (vida)
        {
            case 3:
                imaginador.sprite = full;
                break;
            case 2:
                imaginador.sprite = half;
                break;
            case 1:
                imaginador.sprite = rip;
                break;
            case 0:
                //Se murio
                trinidad.SetActive(false);
                Destroy(gameObject);
                gameOver.GameOver("El trabajo en equipo es recompenzado! Glich Eliminado", (int)EscenasParaCargar.TRINIDAD);
                break;
        }
    }

    public GameOverScript gameOver;
}
