using UnityEngine;
using UnityEngine.UI;

public class ControladorDeSonido : MonoBehaviour
{
    public AudioSource audio;
    public Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(delegate { CambioDeVolumenGeneral(); });
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("volumenGeneral"))
        {
            slider.value = PlayerPrefs.GetFloat("volumenGeneral");
        }
        else
        {
            slider.value = 1;
        }
        audio.volume = slider.value;
    }
    //por cada audio se va a tener uno de estos
    public void CambioDeVolumenGeneral()
    {
        audio.volume = slider.value;
        PlayerPrefs.SetFloat("volumenGeneral", slider.value);
    }
}