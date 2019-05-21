using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [HideInInspector]
    public List<Object> bullets;

    public Object mine;

    public Transform spawnPoint;
    public Object testBullet;
    public Object rocket;
   //public Object mine;
    private ParticleSystem turretSmoke;

    public int activeBullet = 0;

    private GameMode gameMode;

    private void Start()
    {
        //this gets the turret smoke particle system on the turret object
        turretSmoke = GetComponentInChildren<ParticleSystem>();

        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();

        if (gameMode.bullets[0] == null)
        {
            bullets.Add(testBullet);
        }
        else
        {
            bullets.AddRange(gameMode.bullets);
            print(bullets[0]);
        }
    }

    void Update()
    { 
        if (Input.GetButtonDown("Inventory1"))
        {
            activeBullet = 0;
        }
        if (Input.GetButtonDown("Inventory2"))
        {
            activeBullet = 1;
        }
        if (Input.GetButtonDown("Inventory3"))
        {
            activeBullet = 2;
        }


        //this shoots the bullet
        if (Input.GetButtonDown("Fire1"))
        {

            Instantiate(bullets[activeBullet], spawnPoint.position, spawnPoint.rotation);
            //play particle effect
            turretSmoke.Play();
        }

        //this places a mine
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(mine, gameObject.transform.position, gameObject.transform.rotation);
        }

    }
}
