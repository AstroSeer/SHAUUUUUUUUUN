using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public bool patrol = true;
    public Rigidbody2D rigid;
    public float speed = 5;
    static public float waitingConst = 120;

    private bool needWait = false;
    private float waitingTime = waitingConst;

    public Animator guardWalk;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol && !needWait)
        {
            //Debug.Log("I am in patrol mode");
            doPatrol();
        }
        if(needWait)
        {
            waitingTime = waitingTime - 1;
            guardWalk.SetBool("IsMoving",false);
            if(waitingTime == 0)
            {
                needWait = false;
                patrol = true;
                waitingTime = waitingConst;
                reverse();
            }
        }
    }

    void doPatrol()
    {
        rigid.velocity = new Vector2(speed * Time.fixedDeltaTime, rigid.velocity.y);
        guardWalk.SetBool("IsMoving",true);
    }

    void reverse()
    {
        patrol = false;
        speed = speed * -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        patrol = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Patrol")
        {
            needWait = true;
            patrol = false;
        }
    }
}
