using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using System;

public class InstanciadorBoss : MonoBehaviour
    {

        public GameObject Caja;
        public GameObject CajaPinchos;
        GameObject[] Cajas;
        public Transform instanciadorDeCajas;
        [SerializeField] int rangoDeFuerza;
        int probabilidadDeCaja;
        int numeroRandom;
        private float deltaTimeLocal;

        public IACursor iaCursor;
        public float colocarCajas = 0.5f;


    // Start is called before the first frame update
    void Start()
        {
            instanciadorDeCajas = GetComponent<Transform>();
            numeroRandom = Random.Range(2, 5);

        }

       void FixedUpdate()
        {
            deltaTimeLocal += Time.deltaTime;

            if (deltaTimeLocal >= colocarCajas)
            {
            deltaTimeLocal = 0;
            numeroRandom = Random.Range(2, 5);
            contarCajas();
            }
        }

        void instanciarCajas()
        {
            probabilidadDeCaja = Random.Range(0, 100);
            GameObject instanciaDeCaja;
            if (probabilidadDeCaja >= 50)
            {
            instanciaDeCaja= Instantiate(Caja, new Vector3(instanciadorDeCajas.position.x, instanciadorDeCajas.position.y, instanciadorDeCajas.position.z), Quaternion.identity);
            }
            else
            {
            instanciaDeCaja = Instantiate(CajaPinchos, new Vector3(instanciadorDeCajas.position.x, instanciadorDeCajas.position.y, instanciadorDeCajas.position.z), Quaternion.identity);

            }
        instanciaDeCaja.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(rangoDeFuerza * -1, rangoDeFuerza), Random.Range(rangoDeFuerza * -1, rangoDeFuerza)));
        }

        async void contarCajas()
        {

            GameObject[] cajasInstanciadas = GameObject.FindGameObjectsWithTag("CajaInstanciada");

            if (cajasInstanciadas.Length < 15 && iaCursor.deboCambiarDePosicion)
            {

                instanciarCajas();
            }

            else
            {
                iaCursor.deboCambiarDePosicion = false;

                if(iaCursor.meHanPegado == false)
                {
                    await EsperarQueAcabeLaEscena();
                    foreach (GameObject cajas in cajasInstanciadas)
                    {
                        DestroyImmediate(cajas);
                    }

                iaCursor.deboCambiarDePosicion = true;
                if(iaCursor.colliderCursor != null) iaCursor.colliderCursor.enabled = true;
            }
            }
        }

        public void destruirCajasInstantaniamente()
        {

            GameObject[] cajasInstanciadas = GameObject.FindGameObjectsWithTag("CajaInstanciada");

                foreach (GameObject cajas in cajasInstanciadas)
                {
                    Destroy(cajas);
                }
    }
            private async Task EsperarQueAcabeLaEscena()
            {

                await Task.Delay(TimeSpan.FromSeconds(8f));

            }
}
