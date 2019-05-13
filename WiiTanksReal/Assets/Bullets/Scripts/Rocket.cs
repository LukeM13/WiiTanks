﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigid;
    Collider collider;
    public float speed = 1.0f;
    public float damage;
    public int maxNumberOfWallHits = 0;
    private int numOfHits;
    private bool destroy;

    // Start is called before the first frame update
    void Start()
    {

        //gets the rigid body 
        rigid = GetComponent<Rigidbody>();
        //gets the collider
        collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {

        //checks to see if we hit the walls too many times
        if (numOfHits >= maxNumberOfWallHits || destroy)
        {
            /*
             * make smoke particles spawn here 
             */

            //destroy this game object if we have hit the wall
            Destroy(gameObject);
        }
        //this moves the posion of the bullet every frame so that it moves strait and has no curve
        //or gravity
        transform.position += transform.forward * Time.deltaTime * speed;
        //Debug.Log("Rocket made");

    }

    //this is called every time the collider hits an object with a collider
    void OnCollisionEnter(Collision col)
    {
        //checks to see if we hit a wall
        if (col.gameObject.tag == "Wall")
        {
            //if we hit a damage able wall we want to damage the wall and destroy the bullet

            //get the damage interface from the scripts on the wall
            List<Damageable> damageScripts; ;
            //returns a list of damageable interface
            GetInterfaces<Damageable>(out damageScripts, col.gameObject);
            //loop through the interfaces and damage the objects
            foreach (Damageable damageObject in damageScripts)
            {
                //apply the damage
                damageObject.damage(damage, this.gameObject);
                //we want to destroy the object after
                destroy = true;
                //return so it doesn't reflect
                return;
            }

            //this gets the reflected vector and sets the forward direction of this object to that vector
            transform.forward = Vector3.Reflect(transform.forward, col.GetContact(0).normal);

            numOfHits++;
        }
        //checks to see if we hit a tank
        else if (col.gameObject.tag == "Tank")
        {
            //same thing as the wall
            List<Damageable> damageScripts;
            GetInterfaces<Damageable>(out damageScripts, col.gameObject);
            foreach (Damageable damageObject in damageScripts)
            {
                destroy = true;
                damageObject.damage(damage, this.gameObject);
            }

        }
    }

    //this is a function that gets the interfaces from all the scripts on an object
    public static void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class
    {
        MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
        resultList = new List<T>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is T)
            {
                //found one
                resultList.Add((T)((System.Object)mb));
            }
        }
    }
}
