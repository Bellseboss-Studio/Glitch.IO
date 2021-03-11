using UnityEngine;

public class PoderDeDiseñador : ControladorDePoder
{
    public Animator anim_trini, rayo;
    public bool haciendoRayo = false;

    public override bool ImplementacionDeLanzarPoder()
    {
        deltaTimeLocal += Time.deltaTime;
        bool respuesta = deltaTimeLocal >= tiempoDeLanzarPoder;
        if (respuesta) deltaTimeLocal = 0;
        return respuesta;
    }

    protected override void Update()
    {
        if (ImplementacionDeLanzarPoder())
        {
            //Debuug.Log("tiro rayo");

                //Debuug.Log("debo de cargar el rayo si o si");
                anim_trini.SetFloat("rayo", 0.2f);

                anim_trini.SetFloat("rayo", 0.0f);
            rayo.SetBool("lanzoRayo", true);

            //Animar para lanzar rayo.
            //funcion de terminar animacion.
            //lanzamos poder
            //Debuug.Log("Lanzamos el poder del" + this.name);
        }
    }
}
