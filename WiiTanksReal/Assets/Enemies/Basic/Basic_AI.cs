using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Basic_AI : MonoBehaviour, Damageable
{
    //this is used to move the player around and navigation in general
    private NavMeshAgent navAgent;
    //the death particle that plays when killed
    public ParticleSystem deathParticle;
    //the transform of the turret
    public Transform turretTransform;
    public float health;
    //how fast the turret should snap to the player
    public float snappingSpeed;
    //the number of bullets that should be shot in one burst
    public int burstAmount;
    //the players in the game
    private GameObject[] players;
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
    //the spawn point for the bullets
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //gets the navmeshagent
        navAgent = GetComponent<NavMeshAgent>();
        //get all players in the world
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //this gets the distance that the nav agent has left to move on its path
        float dist = navAgent.remainingDistance;
        //this checks to see if we have finished running our path
        if (dist != Mathf.Infinity && navAgent.pathStatus == NavMeshPathStatus.PathComplete && navAgent.remainingDistance == 0)
        {
            //this gets a random location on the mesh and moves to it
            Vector3 randomDirection = Random.insideUnitSphere * 40;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 40, 1);
            Vector3 finalPosition = hit.position;
            navAgent.SetDestination(finalPosition);
            print("finding new path");
        }

        activeIndex = findPlayer();

        if (activeIndex != -1)
        {
            if (smoothTurret < 1)
            {
                smoothTurret += Time.deltaTime * snappingSpeed;
            }
            turretTransform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - new Vector3(players[activeIndex].transform.position.x, transform.position.y, players[activeIndex].transform.position.z)), smoothTurret);
            if (currentBurstTime >= burstTime)
            {
                Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
                currentBurstTime = 0;
            } else
            {
                currentBurstTime += Time.deltaTime;
            }
            
        } else
        {
            smoothTurret = 0;
        }

    }

    //we we are hit with a bullet
    public void damage(float damage, GameObject other)
    {
        health -= damage;
        print(health);
        if (health <= 0)
        {
            print("Dead!");
            //makes a particle effect that hides the tank disappearing
            Instantiate(deathParticle, transform.position, transform.rotation);
            deathParticle.Play();
            Destroy(gameObject);
        }
    }

    private int findPlayer()
    {
        //get raycast results
        RaycastHit hit;
        //loop through them 
        for(int i = 0; i < players.Length; i++) {
            //shoot a line to that player to see if we hit a wall before we hit the player, if so then we can't see the player and shouldn't aim at it
            if (Physics.Raycast(transform.position, players[i].transform.position - transform.position, out hit, 10000))
            {
                print("test1");
                Debug.DrawLine(transform.position, hit.point, Color.red);
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    return i;
                }
            } else
            {
                print("test");
                Debug.DrawRay(transform.position, players[i].transform.position - transform.position, Color.red);
            }

        }
        return -1;
    }

    private GameObject getClosestPlayer()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Player");
        float dist = float.MaxValue;
        int index = 0;
        for (int i = 0; i < list.Length; i++)
        {
            if (Vector3.Distance(list[i].transform.position, list[index].transform.position)  < dist)
            {
                dist = Vector3.Distance(list[i].transform.position, list[index].transform.position);
                index = i;
            }
        }
        return list[index];
    }
}
