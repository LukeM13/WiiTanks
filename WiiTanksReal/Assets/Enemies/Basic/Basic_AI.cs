using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Basic_AI : AIParent
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
    [Range(0, 2)]
    public float followSpeed;
    //the timer for the bursts
    private float currentBurstTime;
    //the bullet that this tank shoot
    public Object bullet;
    //the spawn point for the bullets
    public Transform spawnPoint;

    private GameObject player;

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

        turretTransform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        //Quaternion.Lerp(turretTransform.rotation, Quaternion.LookRotation(transform.position - new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z)), Mathf.Sin(Time.time));


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
    }
}
