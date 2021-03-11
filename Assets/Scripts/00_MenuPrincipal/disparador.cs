using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class disparador : MonoBehaviour
{
    // Start is called before the first frame update
    public void primernivel()
    {
       SceneManager.LoadScene("Cinematique");
    }

   
}
