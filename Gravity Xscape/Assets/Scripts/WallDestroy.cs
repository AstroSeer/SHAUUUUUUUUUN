using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    private bool selfDie;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        selfDie = GameObject.Find("Thief").GetComponent<PlayerMovement>().hasGravityItem;
        if(selfDie) {
            Debug.Log("I am trying to die");
            Destroy(this.gameObject);
        }
    }
}
