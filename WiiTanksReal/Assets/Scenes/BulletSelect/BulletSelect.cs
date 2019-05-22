using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;

public class BulletSelect : MonoBehaviour
{
    [Header("Data for Tanks")]
    public Object tankUI;

    private GameMode gameMode;

    public VerticalLayoutGroup tankHolder;

    [Header("Bullet Select")]
    public GridLayoutGroup bulletGrid;

    public Object bulletButtonPrefab;

    public SelectedBulletScript[] SelectedBullets;

    [HideInInspector]
    public List<BulletParent> bullets = new List<BulletParent>();

    private int activeIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        
        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();
        //get the tanks 
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

        //for (int i = 0; i < gameMode.numUnlockedBullets; i++)
        //{
        //    //create a new tankUI object for the all the tanks in the level
        //    GameObject bulletButton = (GameObject)Instantiate(bulletButtonPrefab);
        //    //set this transform the the tankholder so that it aligns with everything
        //    bulletButton.transform.SetParent(tankHolder.transform, false);
        //}

    }

    public void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (bullets[i] != null)
            {
                SelectedBullets[i].updateValues(bullets[i]);
            }
            else
            {
                SelectedBullets[i].clearValues();
            }
        }
    }

    public void inputBullet(BulletPrefabScript bulletPressed)
    {
        if (bullets.Count <= 3)
        {
            bullets.Add(bulletPressed.bullet);
        }

    }

    public void ButtonPressed(int index)
    {
        if (bullets[index] != null)
        {
            bullets.RemoveAt(index);
        }
    }

    public void goToNextLevel()
    {
        gameMode.bullets.Clear();
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] != null) {
                gameMode.bullets.Add(bullets[i].gameObject);
            }
            
        }
        gameMode.nextLevel();
    }
}
