using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealingDevice : MonoBehaviour
{
    public AnimatorOverrideController startBuild;
    public AnimatorOverrideController Steal;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = startBuild as RuntimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Steal") {
            GetComponent<Animator>().runtimeAnimatorController = Steal as RuntimeAnimatorController;
            animator.SetTrigger("Start_W");
            animator.SetTrigger("End");
        }
    }
}
