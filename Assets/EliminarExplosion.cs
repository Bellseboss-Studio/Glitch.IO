using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(eliminarParticula());
    }

    IEnumerator eliminarParticula()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
