﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    //[HideInInspector]
    public List<Object> bullets;

    public Object mine;

    public Transform spawnPoint;
    public Object Bullet;
    public Object rocket;
   //public Object mine;
    private ParticleSystem turretSmoke;

    public int activeBullet = 0;

    public float stopTime;

    private float stopTimer;

    private List<GameObject> bulletsInWorld = new List<GameObject>();

    private GameMode gameMode;

    [HideInInspector]
    public bool shouldMove = true;

    private void Start()
    {
        //this gets the turret smoke particle system on the turret object
        turretSmoke = GetComponentInChildren<ParticleSystem>();

        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();

    }

    void Update()
    {
    


        //this shoots the bullet
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < bulletsInWorld.Count; i++)
            {
                if (bulletsInWorld[i] == null)
                {
                    bulletsInWorld.RemoveAt(i);
                }
            }
            if (bulletsInWorld.Count < 5)
            {
                shouldMove = false;
                stopTimer = 0;
                bulletsInWorld.Add((GameObject)Instantiate(Bullet, spawnPoint.position, spawnPoint.rotation));
                //play particle effect
                turretSmoke.Play();

            }
        }

        //this places a mine
        if (Input.GetButtonDown("Fire2"))
        {
            shouldMove = false;
            stopTimer = 0;
            Instantiate(mine, gameObject.transform.position, gameObject.transform.rotation);
        }

        if (stopTime <= stopTimer)
        {
            shouldMove = true;
        }
        else
        {
            stopTimer += Time.deltaTime;
        }
    }
}
