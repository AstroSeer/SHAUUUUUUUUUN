using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    GameObject Player;
    // Start is called before the first frame update
    void Start() {
        Player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update() {
    }

    // Checks if player is touching grass
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Ground") {
            Player.GetComponent<PlayerMovement>().grounded = true;
        }
    }

    // Checks if player is not touching grass
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground") {
            Player.GetComponent<PlayerMovement>().grounded = false;
        }
    }
}
