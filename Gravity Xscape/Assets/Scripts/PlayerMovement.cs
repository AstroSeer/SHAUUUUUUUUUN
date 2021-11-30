using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float playerMovementSpeed = 6f;
    public float playerJumpHeight = 10f;
    public bool grounded = false;
    public bool wallLeft = false;
    public bool wallRight = false;

    public bool gravityShift = false;
    public bool canShift = false;
    
    public int keys = 0;
    public bool keysAdd = false;
    public bool keysSub = false;

    public Animator animator;
    public AudioSource p_audio;
    public AudioClip gravity_sfx;
    public AudioClip walk_sfx;

    void Start()
    {
        p_audio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Jump();
        // Horizontal input causes character to move at playerMovementSpeed's value
        if(keysAdd)
        {
            keys = keys + 1;
            keysAdd = false;
            Debug.Log("Keys are now " + keys);
        }
        if(keysSub)
        {
            keys = keys - 1;
            keysSub = false;
            Debug.Log("Door opened, keys are now " + keys);
            wallLeft = false;
            wallRight = false;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        if ((move.x < 0 || move.x > 0) && !p_audio.isPlaying && grounded)
            p_audio.PlayOneShot(walk_sfx, 1.0f);
        if(move.x < 0 && wallLeft) {
            move.x = 0;
        }
        else if (move.x > 0 && wallRight)
        {
            move.x = 0;
        }
        else
        {
            transform.position += move * Time.deltaTime * playerMovementSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canShift)
        {
            gravityShift = !gravityShift;
            canShift = !canShift;
            p_audio.PlayOneShot(gravity_sfx, .7f);
            playerJumpHeight = playerJumpHeight * -1;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale * -1;
            Debug.Log(gravityShift);
            Vector3 characterScaleY = transform.localScale;
            if (gravityShift == true)
            {
                characterScaleY.y = -.1f;
            }
            if (gravityShift == false)
            {
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
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScaleX.x = -.1f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScaleX.x = .1f;
        }
        transform.localScale = characterScaleX;
        // Walk & Idle Animation
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    // Method for 
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerJumpHeight), ForceMode2D.Impulse);
            animator.SetTrigger("IsJumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "NextLevel") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(collision.collider.tag == "OutOfBounds") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.collider.tag == "Key")
        {
            keysAdd = true;
            Destroy(collision.gameObject);
        }
        if (collision.collider.tag == "Door")
        {
            float otherPos = collision.gameObject.transform.position.x;
            float playerPos = transform.position.x;
            float diff = playerPos - otherPos;

            if (diff > 0)
            {
                wallLeft = true;
            }
            else if (diff < 0)
            {
                wallRight = true;
            }

            if (keys > 0)
            {
                keysSub = true;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Door")
        {
            wallLeft = false;
            wallRight = false;
        }
    }
}