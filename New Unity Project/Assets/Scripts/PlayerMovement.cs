using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float playerMovementSpeed = 6f;
    public float playerJumpHeight = 1000f;
    public bool grounded = false;
    public bool wallLeft = false;
    public bool wallRight = false;
    public bool gravityShift = false;
    public Animator animator;
    // Start is called before the first frame update

    public AudioSource p_audio;
    public AudioClip gravity_sfx;
    public AudioClip walk_sfx;

    void Start() {

        p_audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
        Jump();
        // Horizontal input causes character to move at playerMovementSpeed's value
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if ((move.x < 0 || move.x > 0) && !p_audio.isPlaying && grounded)
            p_audio.PlayOneShot(walk_sfx, 1.0f);
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
            p_audio.PlayOneShot(gravity_sfx, .7f);
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
        if (Input.GetKeyDown(KeyCode.X)) {
            animator.SetTrigger("Death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    }

    // Method for 
    void Jump() {
        if(Input.GetButtonDown("Jump") && grounded == true) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerJumpHeight), ForceMode2D.Impulse);
        }
    }

    //NextLevel
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "NextLevel") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(collider.tag == "OutOfBounds") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}