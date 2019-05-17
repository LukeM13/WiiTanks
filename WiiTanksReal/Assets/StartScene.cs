using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("next scene");
    }
    public void music()
    {
        GetComponent<AudioSource>().Play();
    }
}
