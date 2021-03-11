using System.Collections;
using UnityEngine;
using Cinemachine;

    public class CinemachineShake : MonoBehaviour
    {
    public float amplitudeGain;
    public float frequemcyGain;
    public CinemachineFreeLook cmFreeCam;
    public float shakeDuration;

    private void Start()
    {
        cmFreeCam = GetComponent<CinemachineFreeLook>();
    }

    public void DoShake()
    {
        StartCoroutine(Shake());
    }
    public IEnumerator Shake()
    {
        Noise(amplitudeGain, frequemcyGain);
        yield return new WaitForSeconds(shakeDuration);
        Noise(0, 0);
    }
    public void Noise(float amplitude, float frequency)
    {
        cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
    }
}
