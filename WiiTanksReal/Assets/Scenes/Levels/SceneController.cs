using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    GameMode gameMode;

    private int numberOfAI;

    private Animator transition;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfAI = GameObject.FindGameObjectsWithTag("Tank").Length;

        gameMode = GameObject.FindWithTag("GameMode").GetComponent<GameMode>();

        transition = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfAI = GameObject.FindGameObjectsWithTag("Tank").Length;
        if (numberOfAI <= 0)
        {
            StartCoroutine(SceneFadeToTransition());
        }

        if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
        {
            StartCoroutine(SceneFadeToRestart());
        }
    }

    IEnumerator SceneFadeToTransition()
    {
        transition.SetTrigger("FadeOut");
        print("fade out");
        yield return new WaitForSeconds(.8f);
        gameMode.loadTransitionScene();
    }

    IEnumerator SceneFadeToRestart()
    {
        transition.SetTrigger("FadeOut");
        print("fade out");
        yield return new WaitForSeconds(.8f);
        gameMode.restartLevel();
    }
}
