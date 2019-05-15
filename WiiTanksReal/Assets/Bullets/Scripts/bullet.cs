using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 1.0f;
    public float damage;
    public int maxNumberOfWallHits = 1;
    public int numOfHits;
    private bool destroy;

    private float defaultY;

    // Start is called before the first frame update
    protected void Start()
    {
        defaultY = transform.position.y;
    }

    // Update is called once per frame
    protected void Update()
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
        transform.position += new Vector3(transform.forward.x * Time.deltaTime * speed, 0 , transform.forward.z * Time.deltaTime * speed);

    }

    //this is called every time the collider hits an object with a collider
    protected void OnCollisionEnter(Collision col)
    {
        
        //checks to see if we hit a wall
        if (col.gameObject.tag.Equals("Wall"))
        {
            //if we hit a damage able wall we want to damage the wall and destroy the bullet

            //get the damage interface from the scripts on the wall
            List<Damageable> damageScripts; ;
            //returns a list of damageable interface
            GlobalFunctions.GetInterfaces<Damageable>(out damageScripts, col.gameObject);
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
            transform.forward = new Vector3(Vector3.Reflect(transform.forward, col.GetContact(0).normal).x, 0, Vector3.Reflect(transform.forward, col.GetContact(0).normal).z);
            print("collide");

            numOfHits++;
        }
        //checks to see if we hit a tank
        else if (col.gameObject.tag.Equals("Tank"))
        {
            //same thing as the wall
            List<Damageable> damageScripts;
            GlobalFunctions.GetInterfaces<Damageable>(out damageScripts, col.gameObject); 
            foreach (Damageable damageObject in damageScripts)
            {
                destroy = true;
                damageObject.damage(damage, this.gameObject);
            }
            
        } else if (col.gameObject.tag.Equals("Player"))
        {
            //same thing as the wall
            List<Damageable> damageScripts;
            GlobalFunctions.GetInterfaces<Damageable>(out damageScripts, col.gameObject);
            foreach (Damageable damageObject in damageScripts)
            {
                destroy = true;
                damageObject.damage(damage, this.gameObject);
            }
        }
    }
}
