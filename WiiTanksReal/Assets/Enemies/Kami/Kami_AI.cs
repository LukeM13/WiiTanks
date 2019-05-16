using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kami_AI : MonoBehaviour, Damageable
{
    //this is used to move the player around and navigation in general
    private NavMeshAgent navAgent;
    //the death particle that plays when killed
    public ParticleSystem deathParticle;

    public Object explosion;

    public float health;
    //the players in the game
    private GameObject[] players;

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

        navAgent.SetDestination(getClosestPlayer().transform.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
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
        for (int i = 0; i < players.Length; i++)
        {
            //shoot a line to that player to see if we hit a wall before we hit the player, if so then we can't see the player and shouldn't aim at it
            if (Physics.Raycast(transform.position, players[i].transform.position - transform.position, out hit, 10000))
            {

                Debug.DrawLine(transform.position, hit.point, Color.red);
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    return i;
                }
            }
            else
            {

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
            if (Vector3.Distance(list[i].transform.position, list[index].transform.position) < dist)
            {
                dist = Vector3.Distance(list[i].transform.position, list[index].transform.position);
                index = i;
            }
        }
        return list[index];
    }
}
