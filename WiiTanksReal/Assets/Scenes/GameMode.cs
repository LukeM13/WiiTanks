using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


[Serializable]
public struct LevelData
{
    [SerializeField]
    public String scene;
    [SerializeField]
    public Image sceneImage;
    [SerializeField]
    public int numberOfTanks;
}

public class GameMode : MonoBehaviour
{

    public String transitionScene;
    private static GameMode gameMode = null;
    [SerializeField]
    public List<LevelData> gameScenes = new List<LevelData>();

    [HideInInspector]
    public List<UnityEngine.Object> bullets;

    [HideInInspector]
    public int numUnlockedBullets = 1;

    public int currentLevel = -1;

    void Awake()
    {

        if (gameMode == null)
        {
            gameMode = this;
            DontDestroyOnLoad(gameObject);

            //Initialization code goes here[/INDENT]
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void loadTransitionScene()
    {
        currentLevel++;
        numUnlockedBullets = Mathf.Clamp(numUnlockedBullets + 1, 1, bullets.Count);
        SceneManager.LoadScene(transitionScene);
    }

    public void restartLevel()
    {
        
        SceneManager.LoadScene(transitionScene);
    }

    public void nextLevel()
    { 
        SceneManager.LoadScene(gameScenes[currentLevel].scene);
    }

    public LevelData getNextLevelInfo ()
    {
        return gameScenes[currentLevel];
    }

}
