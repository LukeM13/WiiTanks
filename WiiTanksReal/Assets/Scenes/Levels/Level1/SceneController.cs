using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    GameMode gameMode;
    
    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.FindWithTag("GameMode").GetComponent<GameMode>();
    }

    // Update is called once per frame
    void Update()
    {
        gameMode.nextScene();
    }
}
