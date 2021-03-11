using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCanvas : MonoBehaviour
{
    bool mFaded = false;
    public float duracionFadeTexto = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FadeText()
    {
        var canvas = GetComponent<CanvasGroup>();
        StartCoroutine(HacerElFade(canvas, canvas.alpha, mFaded ? 1 : 0));
        mFaded = !mFaded;
    }

    public IEnumerator HacerElFade(CanvasGroup canvas, float start, float end)
    {
        float counter = 0f;

        while (counter < duracionFadeTexto)
        {
            counter += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(start, end, counter / duracionFadeTexto);

            yield return null;
        }
    }
}
