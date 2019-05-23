using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[Serializable]
public enum TankType
{
    basic,
    Rocket,
    gonzales,
    Mineplacer,
    kami,
}
[Serializable]
public struct TankData
{
    [SerializeField]
    public TankType type;
    [SerializeField]
    public int number;
}

[Serializable]
public struct LevelData
{
    [SerializeField]
    public SceneAsset scene;
    [SerializeField]
    public Image sceneImage;
    [SerializeField]
    public List<TankData> tankInfo;
}

public class GameMode : MonoBehaviour
{

    public SceneAsset transitionScene;
    private static GameMode gameMode = null;
    [SerializeField]
    public List<LevelData> gameScenes = new List<LevelData>();

    [HideInInspector]
    public List<UnityEngine.Object> bullets;

    [HideInInspector]
    public int numUnlockedBullets = 1;

    public int currentLevel;

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
        SceneManager.LoadScene(transitionScene.name);
    }

    public void loadFirstLevel()
    {
        SceneManager.LoadScene(gameScenes[0].scene.name);
    }

    public void restartLevel()
    {
        
        SceneManager.LoadScene(transitionScene.name);
    }

    public void nextLevel()
    { 
        SceneManager.LoadScene(gameScenes[currentLevel].scene.name);
    }

    public LevelData getNextLevelInfo ()
    {
        return gameScenes[currentLevel + 1];
    }

}
