using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    Rigidbody rigid;
    Collider collider;
    public float speed = 1.0f;
    public int maxNumberOfWallHits = 0;
    private int numOfHits;

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
        if (numOfHits >= maxNumberOfWallHits)
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

    }

    //this is called every time the collider hits an object with a collider
    void OnCollisionEnter(Collision col)
    {
        //checks to see if we hit a wall
        if (col.gameObject.tag == "Wall")
        {
            //this gets the reflected vector and sets the forward direction of this object to that vector
            transform.forward = Vector3.Reflect(transform.forward, col.GetContact(0).normal);
            numOfHits++;
        } 
        //checks to see if we hit a tank
        else if (col.gameObject.tag == "Tank")
        {

        }
    }
}
