using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{

    StealingDevice DeviceCheck;

    public bool isEscaping;

    public bool hasLooped;

    public AudioSource l_audioSource;

    public AudioClip escapeDrum_ost;

    public AudioClip escape_ost;

    public AudioClip inflitrate_ost;


    // Start is called before the first frame update
    void Start()
    {
        l_audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);

        l_audioSource.clip = inflitrate_ost;

        l_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        isEscaping = GameObject.Find("Thief").GetComponent<PlayerMovement>().hasGravityItem;
        
        if (isEscaping == true)
        {
            if (hasLooped)
            {
                l_audioSource.loop = true;
                if (!l_audioSource.isPlaying)
                {
                    l_audioSource.loop = true;
                    l_audioSource.clip = escape_ost; ;
                    l_audioSource.Play();
                }
            }
            else
            {
                l_audioSource.Stop();
                l_audioSource.loop = false;
                if (!l_audioSource.isPlaying){
                    l_audioSource.PlayOneShot(escapeDrum_ost, 1);
                    hasLooped = true;
                }
            }
        }
    }
}
