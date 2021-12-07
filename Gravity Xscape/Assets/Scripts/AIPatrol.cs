using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public bool patrol;
    public Rigidbody2D rigid;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        patrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(patrol)
        {
            doPatrol();
        }    
    }

    void doPatrol()
    {
        rigid.velocity = new Vector2(speed * Time.fixedDeltaTime, rigid.velocity.y);
    }

    void reverse()
    {
        patrol = false;
        speed = speed * -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        patrol = true;
    }
}
