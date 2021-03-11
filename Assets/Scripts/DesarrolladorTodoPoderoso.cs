using UnityEngine;

public class DesarrolladorTodoPoderoso : MonoBehaviour
{
    [SerializeField]private float tiempoDeUtilizacionDe;//este debe ser entre 10 y 15 seg
    [SerializeField] private GameObject player;

    [SerializeField]float deltaTimeLocal;
    bool poder;
    // Start is called before the first frame update
    void Start()
    {
        tiempoDeUtilizacionDe = UnityEngine.Random.Range(10, 15);
        poder = true;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTimeLocal += Time.deltaTime;
        if (deltaTimeLocal >= tiempoDeUtilizacionDe)
        {
            deltaTimeLocal = 0;
            if (poder)
            {
                poder = false;
                //Debuug.Log("cuarto");
                CambiandoleLaMecanicaAlPlayer();
                tiempoDeUtilizacionDe = UnityEngine.Random.Range(2, 10);
            }
            else
            {
                poder = true;
                ColocandoTodoNormalParaElPlayer();
                tiempoDeUtilizacionDe = UnityEngine.Random.Range(10, 15);
            }
        }
    }

    private void CambiandoleLaMecanicaAlPlayer()
    {
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                player.GetComponent<CharacterController2D>().puedeSaltar = false;
                break;
            case 1:
                player.GetComponent<CharacterController2D>().puedeIrHaciaDerecha = false;
                break;
            case 2:
                player.GetComponent<CharacterController2D>().puedeIrHaciaIzquierda = false;
                break;
            default:
                break;
        }
        tiempoDeUtilizacionDe = UnityEngine.Random.Range(2, 10);
        //Debuug.Log("primero");
        //Debuug.Log("Segundo");
    }

    private void ColocandoTodoNormalParaElPlayer()
    {
        player.GetComponent<CharacterController2D>().puedeSaltar = true;
        player.GetComponent<CharacterController2D>().puedeIrHaciaDerecha = true;
        player.GetComponent<CharacterController2D>().puedeIrHaciaIzquierda = true;
        tiempoDeUtilizacionDe = UnityEngine.Random.Range(10, 15);
    }
}
