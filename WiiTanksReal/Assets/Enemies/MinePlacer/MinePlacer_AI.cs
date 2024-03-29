﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class MinePlacer_AI : AIParent
{

    //the transform of the turret
    public Transform turretTransform;
    //how fast the turret should snap to the player
    public float snappingSpeed;
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
    //the bullet that this tank shoot
    public Object bullet;
    //the mine to place
    public Object mine;

    public float mineTimeMin;
    public float mineTimeMax;
    private float mineTime;
    private float minePlaceTimer;


    //the spawn point for the bullets
    public Transform spawnPoint;

    // Update is called once per frame
    void Update()
    {
        player = getClosestPlayer();
        //this gets the distance that the nav agent has left to move on its path
        float dist = navAgent.remainingDistance;
        //this checks to see if we have finished running our path
        if (dist != Mathf.Infinity && ((navAgent.pathStatus == NavMeshPathStatus.PathComplete && navAgent.remainingDistance == 0) || navAgent.remainingDistance < navAgent.radius))
        {
            //this gets a random location on the mesh and moves to it
            Vector3 randomDirection = Random.insideUnitSphere * 20;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 20, 1);
            Vector3 finalPosition = hit.position;
            navAgent.SetDestination(finalPosition);
        }

        if (minePlaceTimer >= mineTime)
        {
            Instantiate(mine, transform.position, transform.rotation);
            mineTime = Random.Range(mineTimeMin, mineTimeMax);
            minePlaceTimer = 0;
        } else
        {
            minePlaceTimer += Time.deltaTime;
        }

        if (canSeePlayer(player))
        {
            if (currentBurstTime >= burstTime)
            {
                Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
                currentBurstTime = 0;
            }
            else
            {
                currentBurstTime += Time.deltaTime;
            }

        }
        turretTransform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
    }
}
