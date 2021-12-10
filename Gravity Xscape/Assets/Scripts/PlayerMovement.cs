using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float playerMovementSpeed = 6f;
    public float playerJumpHeight = 10f;
    public bool grounded = false;
    public bool wallLeft = false;
    public bool wallRight = false;

    public bool gravityShift = false;
    public bool canShift = false;
    public bool hasGravityItem = false;

    public int keys = 0;
    public bool keysAdd = false;
    public bool keysSub = false;

    public Animator animator;
    public Animator W_Flash;
    public GameObject Shift;
    public GameObject Guard1;
    public GameObject Guard2;
    public GameObject Guard3;
    public GameObject Guard4;

    public AudioSource p_audioSource;

    public AudioClip walking_sfx;
    public AudioClip gravity_sfx;

    public AudioClip death_sfx;

    void Start()
    {
       if(SceneManager.GetActiveScene().buildIndex != 1)
       {
           hasGravityItem = true;
       } 
       p_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() 
    {
        //Debug.Log("");
        Jump();
        //Debug.Log("Grounded is currently "+grounded);
        // Horizontal input causes character to move at playerMovementSpeed's value
        if(keysAdd)
        {
            keys = keys + 1;
            keysAdd = false;
            //Debug.Log("Keys are now " + keys);
        }
        if(keysSub)
        {
            keys = keys - 1;
            keysSub = false;
            //Debug.Log("Door opened, keys are now " + keys);
            wallLeft = false;
            wallRight = false;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        if(move.x < 0 && wallLeft) 
        {
            move.x = 0;
        }
        else if(move.x > 0 && wallRight) 
        {
            move.x = 0;
        }
        else 
        {
            transform.position += move * Time.deltaTime * playerMovementSpeed;
        }

        if ((move.x < 0 || move.x > 0) && !p_audioSource.isPlaying && grounded == true)
        {
            p_audioSource.PlayOneShot(walking_sfx, .75f);
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && canShift && hasGravityItem) 
        {
            gravityShift = !gravityShift;
            canShift = !canShift;
            playerJumpHeight = playerJumpHeight * -1;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale * -1;
            p_audioSource.PlayOneShot(gravity_sfx, 1);
            //Debug.Log("Gravity shift is "+gravityShift);
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
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.X)){
            animator.SetTrigger("Death");
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
        //RestartShortcut
    }

    // Method for 
    void Jump() {
        if(((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded == true) 
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, playerJumpHeight), ForceMode2D.Impulse);
            animator.SetTrigger("IsJumping");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
    //NextLevel
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "NextLevel") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(collider.tag == "OutOfBounds") {
            //Debug.Log("Die");
            p_audioSource.PlayOneShot(death_sfx, 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collider.tag == "Steal") {
            hasGravityItem = true;
            Debug.Log("Gravity item is now "+ hasGravityItem);
            Shift.SetActive(true);
            Guard1.SetActive(true);
            Guard2.SetActive(true);
            Guard3.SetActive(true);
            Guard4.SetActive(true);
            W_Flash.SetTrigger("Start_W");
            W_Flash.SetTrigger("End");
        }
        if(collider.tag == "Death")
        {
            Time.timeScale = 1f;
            p_audioSource.PlayOneShot(death_sfx, 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}