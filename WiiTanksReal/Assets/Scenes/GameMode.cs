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
    gonzales,
    kami
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

    private static GameMode gameMode = null;
    [SerializeField]
    public List<LevelData> gameScenes = new List<LevelData>();
    
    private static int currentLevel = 0;

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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       // if(gameScenes.GetType
    }

    public void nextScene()
    {
        //print("Loading Scene " + gameScenes[currentLevel].scene.name);
        //currentLevel++;
        //SceneManager.LoadScene(gameScenes[currentLevel].scene.name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
