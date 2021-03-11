using UnityEngine;

public abstract class ControladorDePoder : MonoBehaviour
{
    protected float deltaTimeLocal;
    [SerializeField]protected float tiempoDeLanzarPoder;

    public abstract bool ImplementacionDeLanzarPoder();
    protected abstract void Update();

}
