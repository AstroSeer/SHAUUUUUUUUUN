using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMusicVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer musicMixer;

    public void setLevel (float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
