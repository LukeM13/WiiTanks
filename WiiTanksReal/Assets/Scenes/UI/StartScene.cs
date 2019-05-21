using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public AudioClip audio;
    // Start is called before the first frame update
    public void play()
    {
        DontDestroyOnLoad(audio);
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("next scene");
    }
    public void music()
    {
        //GetComponent<AudioSource>().Play();
    }
}
