using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public string parameterName;
    public AudioMixer mixer;

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat(parameterName, Mathf.Log10(sliderValue) * 20);
    }
}