using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBGFlip : MonoBehaviour {
    public GameObject player;

    void Update() {
        this.transform.position = player.transform.position;
    }
}
