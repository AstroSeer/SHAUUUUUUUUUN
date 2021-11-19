using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour {
    GameObject Player;
    // Start is called before the first frame update
    void Start() {
        Player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        float otherPos = collision.gameObject.transform.position.x;
        float selfPos = Player.transform.position.x;
        if(collision.collider.tag == "Wall") {
            float diff = selfPos - otherPos;
            //Debug.Log("otherPos is " + otherPos);
            //Debug.Log("selfPos is " + selfPos);
            //Debug.Log("diff is " + diff);
            if (diff < 0) {
                Player.GetComponent<PlayerMovement>().wallRight = true;
            }
            else {
                Player.GetComponent<PlayerMovement>().wallLeft = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Wall") {
            Player.GetComponent<PlayerMovement>().wallRight = false;
            Player.GetComponent<PlayerMovement>().wallLeft = false;
        }
    }
}