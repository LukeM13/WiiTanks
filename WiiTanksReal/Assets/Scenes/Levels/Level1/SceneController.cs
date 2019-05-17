﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    GameMode gameMode;

    private int numberOfAI;
    
    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.FindWithTag("GameMode").GetComponent<GameMode>();

        numberOfAI = GameObject.FindGameObjectsWithTag("Tank").Length;
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void tankKill()
    {
        numberOfAI--;
        if (numberOfAI <= 0)
        {
            gameMode.loadBulletSelect();
        }
    }

    public void playerKill()
    {

    }
}