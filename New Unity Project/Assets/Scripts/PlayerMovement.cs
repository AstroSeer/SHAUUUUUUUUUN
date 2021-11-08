using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float playerMovementSpeed = 6f;
    public float playerJumpHeight = 6f;
    public bool grounded = false;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Jump();
        // Horizontal input causes character to move at playerMovementSpeed's value
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += move * Time.deltaTime * playerMovementSpeed;
    }

    // Method for 
    void Jump() {
        if(Input.GetButtonDown("Jump") && grounded == true) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerJumpHeight), ForceMode2D.Impulse);
        }
    }
}
