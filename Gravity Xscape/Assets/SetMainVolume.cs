using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMainVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer musicMixer;

    public void setLevel(float sliderValue)
    {
        musicMixer.SetFloat("GameVol", Mathf.Log10 (sliderValue) * 20);
    }
}
