using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Smart_AI : AIParent
{
    [Header("Aiming")]
    //the transform of the turret
    public Transform turretTransform;
    //how fast the turret should snap to the player
    public float followSpeed;
    //the number of bullets that should be shot in one burst
    public int burstAmount;
    //this index of the player we can see
    private int activeIndex = -1;
    //the timer for moving the turret
    private float smoothTurret = 0;
    //the time between each burst of bullets
    public float burstTime;
    //the timer for the bursts
    private float currentBurstTime;
    [Header("Shooting")]
    //the bullet that this tank shoot
    public Object bullet;
    //the spawn point for the bullets
    public Transform spawnPoint;
    [Header("Brains Stuff")]
    [Range(1,20)]
    public float minDistFromPlayer;
    private int angleIncrements;

    private GameObject[] boundry;

    private void Start()
    {
        base.Start();

        boundry = GameObject.FindGameObjectsWithTag("Boundry");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = getClosestPlayer();
        if (Vector3.Distance(transform.position, player.transform.position) < minDistFromPlayer)
        {
            if (canSeePlayer(player))
            {
                //snap and start shooting
                //maybe add prediction or something like that
            } else
            {
                //figure out how to make it aim at the wall
                //while (Physics.Raycast(transform.position, (transform.position - (player.transform.position)))
                //{

                //}
            }
        }

    }
}
