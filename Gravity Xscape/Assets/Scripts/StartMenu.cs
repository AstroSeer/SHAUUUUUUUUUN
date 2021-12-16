using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Building");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Credits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }
}