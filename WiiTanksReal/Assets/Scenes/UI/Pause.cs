using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Resume();
        }
        else
        {
            PauseGame();
        }
        Debug.Log("WORK");
    }

    private void PauseGame()
    {
        paused = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    private void Resume()
    {
        paused = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }
}
