using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 60f;

    [SerializeField] TextMeshProUGUI countDown;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countDown.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene("GameOver");
            Destroy(this.gameObject);
        }
        if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 5) 
        {
            Destroy(this.gameObject);
        }
    }
}
