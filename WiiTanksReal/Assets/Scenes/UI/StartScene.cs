using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public AudioClip audio;

    private GameMode gameMode;
    // Start is called before the first frame update
    public void play()
    {
        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();
        DontDestroyOnLoad(audio);
        GetComponent<AudioSource>().Play();
        gameMode.loadFirstLevel();
        Debug.Log("next scene");
    }
    public void music()
    {
        //GetComponent<AudioSource>().Play();
    }
}
