using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject returnButton;
    private bool showReturn;

    public GameObject instructionsMenu;

    private GameMode gameMode;

    public void PlayGame()
    {
        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();
        DontDestroyOnLoad(GetComponent<AudioSource>());
        gameMode.loadFirstLevel();
        Debug.Log("next scene");
    }

    public void openInstructions()
    {
        returnButton.SetActive(true);
        instructionsMenu.SetActive(true);
    }

    public void returnToMenu()
    {
        returnButton.SetActive(false);
        instructionsMenu.SetActive(false);
    }
}
