using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealingDevice : MonoBehaviour
{
    public AnimatorOverrideController startBuild;
    public AnimatorOverrideController Steal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            GetComponent<Animator>().runtimeAnimatorController = startBuild as RuntimeAnimatorController;
            Debug.Log("Pressed");
        }
        if(Input.GetKeyDown("e"))
        {
            GetComponent<Animator>().runtimeAnimatorController = Steal as RuntimeAnimatorController;
            Debug.Log("Revert");
        }
    }
}
