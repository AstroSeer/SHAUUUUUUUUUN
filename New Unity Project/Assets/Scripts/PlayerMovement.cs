using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float playerMovementSpeed = 6f;
    public float playerJumpHeight = 1000f;
    public bool grounded = false;
    public bool wallLeft = false;
    public bool wallRight = false;
    public bool gravityShift = false;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Jump();
        // Horizontal input causes character to move at playerMovementSpeed's value
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if(move.x < 0 && wallLeft) {
            move.x = 0;
        }
        else if(move.x > 0 && wallRight) {
            move.x = 0;
        }
        else {
            transform.position += move * Time.deltaTime * playerMovementSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            gravityShift = !gravityShift;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale * -1;
            Debug.Log(gravityShift);
        }
    }

    // Method for 
    void Jump() {
        if(Input.GetButtonDown("Jump") && grounded == true) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerJumpHeight), ForceMode2D.Impulse);
        }
    }
}
