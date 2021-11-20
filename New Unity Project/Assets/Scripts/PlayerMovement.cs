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
    public Animator animator;
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
            playerJumpHeight = playerJumpHeight * -1;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale * -1;
            Debug.Log(gravityShift);
            Vector3 characterScaleY = transform.localScale;
            if (gravityShift == true){
                characterScaleY.y = -.1f;
            }
            if (gravityShift == false){
                characterScaleY.y = .1f;
            }
            transform.localScale = characterScaleY;
        }
        if (grounded == false) {
            animator.SetBool("IsFalling", true);
        }
        if (grounded == true) {
            animator.SetBool("IsFalling", false);
        }
        // Flip the Character
        Vector3 characterScaleX = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0){
            characterScaleX.x = -.1f;
        }
        if (Input.GetAxis("Horizontal") > 0){
            characterScaleX.x = .1f;
        }
        transform.localScale = characterScaleX;
        // Walk & Idle Animation
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

    }

    // Method for 
    void Jump() {
        if(Input.GetButtonDown("Jump") && grounded == true) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerJumpHeight), ForceMode2D.Impulse);
            animator.SetTrigger("IsJumping");
        }
    }
}