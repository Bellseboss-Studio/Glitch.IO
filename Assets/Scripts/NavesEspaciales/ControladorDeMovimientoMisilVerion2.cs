using UnityEngine;

public class ControladorDeMovimientoMisilVerion2 : MonoBehaviour
{
    public bool arriba;
    public float speed;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = (arriba ? Vector2.right : Vector2.left) * (speed * Time.deltaTime);
    }
}
