using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(collision.collider.tag == "Ground") {
            Player.GetComponent<PlayerMovement>().grounded = true;
            Player.GetComponent<PlayerMovement>().canShift = true;
            p_audio.PlayOneShot(land_sfx, .35f);
        }
    }

    // Checks if player is not touching ground
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground") {
            Player.GetComponent<PlayerMovement>().grounded = false;
        }
    }
}