using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : bullet
{
    //private int numOfHits = 0;
    //private bool destroy = false;
    public Object shard;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Debug.Log("Split Make");
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfHits >= maxNumberOfWallHits || destroy)
        {
            /*
             * make smoke particles spawn here 
             */

            //destroy this game object if we have hit the wall
            //Instantiate(shard, transform);
            Destroy(gameObject);
        }
        //this moves the posion of the bullet every frame so that it moves strait and has no curve
        //or gravity
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            //if we hit a damage able wall we want to damage the wall and destroy the bullet

            //get the damage interface from the scripts on the wall
            List<Damageable> damageScripts; ;
            //returns a list of damageable interface
            GetInterfaces<Damageable>(out damageScripts, collision.gameObject);
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
            Instantiate(shard, transform);
            Destroy(gameObject);

            //this gets the reflected vector and sets the forward direction of this object to that vector
            transform.forward = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);

            numOfHits++;
        }
        //checks to see if we hit a tank
        else if (collision.gameObject.tag == "Tank")
        {
            //same thing as the wall
            List<Damageable> damageScripts;
            GetInterfaces<Damageable>(out damageScripts, collision.gameObject);
            foreach (Damageable damageObject in damageScripts)
            {
                destroy = true;
                damageObject.damage(damage, this.gameObject);
            }

        }
    }
}

