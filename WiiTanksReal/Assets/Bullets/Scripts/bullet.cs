using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class bullet : BulletParent
{


    // Start is called before the first frame update
    protected void Start()
    {
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
            Instantiate(deathParticle, transform.position, Quaternion.Inverse(transform.rotation));
            //destroy this game object if we have hit the wall
            Destroy(gameObject);
        }
        //this moves the posion of the bullet every frame so that it moves strait and has no curve
        //or gravity



        transform.position += transform.forward * Time.deltaTime * speed;
        

    }
}
