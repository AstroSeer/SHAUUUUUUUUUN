using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    private bool selfDie;

    //public AudioSource w_audioSource;

 

    public bool opened;

    // Start is called before the first frame update
    void Start()
    {
        //w_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        selfDie = GameObject.Find("Thief").GetComponent<PlayerMovement>().hasGravityItem;
        if(selfDie) {
            Debug.Log("I am trying to die");
            Destroy(this.gameObject);
            /*
            if (!w_audioSource.isPlaying)
            {
                w_audioSource.Play();
                opened = true;
            }
            if (opened)
            {
                Debug.Log("I am trying to die");
                Destroy(this.gameObject);
            }
            */
        }
    }
}
