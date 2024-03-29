﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kami_AI : AIParent
{

    public Object explosion;

    public float minDist;


    // Update is called once per frame
    void Update()
    {

        navAgent.SetDestination(getClosestPlayer().transform.position);
        if (Vector3.Distance(transform.position, getClosestPlayer().transform.position) < minDist)
        {
            death();
        }
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

    private new void death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        base.death();
    }
}
