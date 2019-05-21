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
    public float ShootTime;
    //the timer for the bursts
    private float currentShootTime;
    [Header("Shooting")]
    //the bullet that this tank shoot
    public Object bullet;
    //the spawn point for the bullets
    public Transform spawnPoint;

    [Header("Brains Stuff")]
    [Range(1,20)]
    public float minDistFromPlayer;
    public int RaysToShoot;

    private Vector3 aimingAngle;

    private GameObject[] boundry;

    public void Start()
    {
        base.Start();

        boundry = GameObject.FindGameObjectsWithTag("Boundry");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = getClosestPlayer();



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

        
        //if (Vector3.Distance(transform.position, player.transform.position) < minDistFromPlayer)
        //{
            if (canSeePlayer(player))
            {
                aimingAngle = player.transform.position - transform.position;
            } else
            {
                float minDist = float.MaxValue;
                for (int i = 0; i < RaysToShoot; i++)
                {
                    Vector3 dir = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + ((360 / RaysToShoot) * i),
                        transform.rotation.z) * transform.forward;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, dir, out hit))
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        if (hit.collider.gameObject.tag.Equals("Player"))
                        {
                            aimingAngle = dir;
                            break;
                        }
                        if (hit.collider.gameObject.tag.Equals("Wall") || hit.collider.gameObject.tag.Equals("Boundry"))
                        {
                            if (Vector3.Distance(hit.point, player.transform.position) < minDist)
                            {

                                aimingAngle = dir;
                                minDist = Vector3.Distance(hit.point, player.transform.position);

                            }
                            RaycastHit reflectHit;
                            Vector3 reflectDir = Vector3.Reflect(dir, hit.normal);
                            if (Physics.Raycast(hit.point, reflectDir, out reflectHit))
                            {
                                if (Vector3.Distance(reflectHit.point, player.transform.position) < minDist)
                                {
                                    aimingAngle = dir;
                                    minDist = Vector3.Distance(reflectHit.point, player.transform.position);
                                }
                                Debug.DrawLine(hit.point, reflectHit.point, Color.black);
                            }
                        }

                    }
                }
            //}
        }

        turretTransform.rotation = Quaternion.LookRotation(aimingAngle);

        if (currentShootTime >= ShootTime)
        {
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            currentShootTime = 0;
        }
        else
        {
            currentShootTime += Time.deltaTime;
        }
    }
}
