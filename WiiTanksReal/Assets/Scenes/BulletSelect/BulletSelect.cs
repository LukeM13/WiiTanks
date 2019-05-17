using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;

public class BulletSelect : MonoBehaviour
{
    public Object tankUI;

    private GameMode gameMode;

    public VerticalLayoutGroup tankHolder;

    // Start is called before the first frame update
    void Start()
    {
        
        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();

        foreach (TankData tank in gameMode.getNextLevelInfo().tankInfo)
        {
           
            //create a new tankUI object for the all the tanks in the level
            GameObject tankObject = (GameObject)Instantiate(tankUI);
            //set this transform the the tankholder so that it aligns with everything
            tankObject.transform.SetParent(tankHolder.transform, false);
            //change the tank values so that the ui reflects this properly
            TankUIScript tankValues = tankObject.GetComponent<TankUIScript>();
            //update these values
            print("tank type: " + tank.type.ToString() +  ", count: " + tank.number);
            tankValues.updateValues(tank.type, tank.number);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToNextLevel()
    {
        gameMode.nextLevel();
    }
}
