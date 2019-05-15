using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneManager : MonoBehaviour
{

    private static SceneManager sceneManager = null;

    void Awake()
    {
        
        if (sceneManager == null)
        {
            sceneManager = this;
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
        
    }
}
