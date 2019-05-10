using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    public float health;

    public float speed;
    public Transform spawnPoint;
    public Object bulletType;

    private CharacterController controller;
    private Rigidbody rigid;
    public Transform bodyTransform;
    private Vector3 moveDir = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        //this gets the character controller that is attached to this object
        controller = GetComponent<CharacterController>();
        //this gets the rigid body from the object
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this shoots the bullet
        if (Input.GetButtonDown("Fire1"))
        {
            //this creates a new object of bullet at the certain position and rotation
            Instantiate(bulletType, spawnPoint.position, spawnPoint.rotation);
        }

        moveTank();
        turnTank();


    }

    //moves tank
    void moveTank()
    {
        //this gets the input from WASD as a double from 0.0 to 1.0 and makes a vector
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //this makes the moveDir vector relative to the actor so that it moves the actor in the right direction
        moveDir = transform.TransformDirection(moveDir);
        /*
         * this actually moves the player by moving in the moveDir * the time between each frame so that 
         * its consistant no matter what the frame rate is 
        */
        controller.Move(moveDir * Time.deltaTime * speed);
    }

    //turns tank
    void turnTank()
    { 

        //figure out how to do this
        if (moveDir.z > 0)
        {
            bodyTransform.rotation = new Quaternion(0, Mathf.LerpAngle(bodyTransform.rotation.y, 0, moveDir.z), 0, bodyTransform.rotation.w);


        }
        if(moveDir.x > 0)
        {
            bodyTransform.rotation = new Quaternion(0, Mathf.LerpAngle(bodyTransform.rotation.y, 90, moveDir.x), 0, bodyTransform.rotation.w);
        }

    }

    //this is called when ever the tank is damaged
    public void damage(float damage, GameObject other)
    {
        health -= damage;
        print("Health is: " + health);
    }

}
