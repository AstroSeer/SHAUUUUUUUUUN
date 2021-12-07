using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (collision.collider.tag == "RightWall")
        {
            Player.GetComponent<PlayerMovement>().wallRight = true;
        }
        else if (collision.collider.tag == "LeftWall")
        {
            Player.GetComponent<PlayerMovement>().wallLeft = true;
        }
        if (collision.collider.tag == "Death")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "RightWall")
        {
            Player.GetComponent<PlayerMovement>().wallRight = false;
        }
        else if (collision.collider.tag == "LeftWall")
        {
            Player.GetComponent<PlayerMovement>().wallLeft = false;
        }
    }
}