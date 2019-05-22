using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kami_AI : AIParent
{

    public Object explosion;

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            death();
        }
    }


    //we we are hit with a bullet
    public override void damage(float damage, GameObject other)
    {
        print("hit with bullet");
        health -= damage;
        if (health <= 0)
        {
            death();
        }
    }

    private void death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        base.death();
    }
}
