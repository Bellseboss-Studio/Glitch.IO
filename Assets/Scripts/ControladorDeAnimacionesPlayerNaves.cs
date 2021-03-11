using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeAnimacionesPlayerNaves : MonoBehaviour
{

    public void TerminoDeMorir()
    {
        SceneManager.LoadScene("NavesEspaciales");
    }

    public void InicioDeMorir()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<ControladorDePlayerGalaga>().enabled = false;
    }

    public void FuncionDummy()
    {

    }
}
