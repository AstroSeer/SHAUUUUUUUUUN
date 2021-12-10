using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundCheck : MonoBehaviour {
    GameObject Player;

    public AudioSource p_audio;

    public AudioClip land_sfx;
    // Start is called before the first frame update
    void Start() {
        Player = gameObject.transform.parent.gameObject;
        p_audio = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Checks if player is touching ground
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Ground" || collision.collider.tag == "Patrol") {
            Debug.Log("I am entering "+collision.collider.tag);
            Player.GetComponent<PlayerMovement>().grounded = true;
            Player.GetComponent<PlayerMovement>().canShift = true;
            p_audio.PlayOneShot(land_sfx, .35f);
        }
        if (collision.collider.tag == "Death")
        {
            Debug.Log("I am dying from "+ collision.collider.tag);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    // Checks if player is not touching ground
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Patrol") {
            Player.GetComponent<PlayerMovement>().grounded = false;
            Debug.Log("I am exiting " + collision.collider.tag);
        }
    }
}