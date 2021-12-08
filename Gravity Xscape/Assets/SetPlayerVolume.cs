using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetPlayerVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer musicMixer;

    public void setLevel(float sliderValue)
    {
        musicMixer.SetFloat("PlayerVol", Mathf.Log10(sliderValue) * 20);
    }
}