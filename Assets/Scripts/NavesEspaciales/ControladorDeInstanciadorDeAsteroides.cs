using System.Collections.Generic;
using UnityEngine;

public class ControladorDeInstanciadorDeAsteroides : MonoBehaviour
{
    [SerializeField]private List<GameObject> asteroides;
    [SerializeField] private float tiempoDeSpaw, alturaMaxima, alturaMinima;
    private float deltaTimeLocal;

    private void Update()
    {
        deltaTimeLocal += Time.deltaTime;
        if(deltaTimeLocal >= tiempoDeSpaw)
        {
            deltaTimeLocal = 0;
            InstanciamosUnAsteroide();
        }
    }

    private void InstanciamosUnAsteroide()
    {
        //Buscamos una posicion a donde la  vamos a instanciar
        Vector2 posicionDeAsteroide = new Vector2(transform.position.x, Random.Range(alturaMinima, alturaMaxima));
        //buscamos un random del asteroide
        Instantiate(asteroides[Random.Range(0, (asteroides.Count-1))]).transform.position = posicionDeAsteroide;

    }
}
