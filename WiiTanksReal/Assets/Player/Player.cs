﻿using System.Collections;
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
    private ParticleSystem turretSmoke;
    public Transform bodyTransform;
    private float vertValue = 0.0f;
    private float horzValue = 0.0f;
    private Vector3 moveDir = Vector3.zero;

    private float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        //this gets the character controller that is attached to this object
        controller = GetComponent<CharacterController>();
        //this gets the rigid body from the object
        rigid = GetComponent<Rigidbody>();
        //this gets the turret smoke particle system on the turret object
        turretSmoke = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the value for moving up and down
        horzValue = Input.GetAxis("Horizontal");
        //set the value for moving left and right
        vertValue = Input.GetAxis("Vertical");

        //this shoots the bullet
        if (Input.GetButtonDown("Fire1"))
        {
            //this creates a new object of bullet at the certain position and rotation
            Instantiate(bulletType, spawnPoint.position, spawnPoint.rotation);
            //play particle effect
            turretSmoke.Play();
        }

        moveTank();
        turnTank();


    }

    //moves tank
    void moveTank()
    {
        //this gets the input from WASD as a double from 0.0 to 1.0 and makes a vector
        moveDir = new Vector3(horzValue, 0, vertValue);
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
        //all these get the direction of movement and get an angle from it
        if (horzValue > 0 && vertValue > 0)
        {
            angle = 45;
        }
        else if(horzValue > 0 && vertValue < 0)
        {
            angle = 135;
        }
        else if (horzValue < 0 && vertValue < 0)
        {
            angle = 215;
        }
        else if (horzValue < 0 && vertValue > 0)
        {
            angle = 315;
        }
        else if (horzValue > 0)
        {
            angle = 90;
        }
        else if (horzValue < 0)
        {
            angle = 270;
        }
        else if (vertValue > 0)
        {
            angle = 0;
        } 
        else if (vertValue < 0)
        {
            angle = 180;
        }

        //this rotates the player to that angle
        if (Mathf.Abs(vertValue) > 0)
        {
            //print(angle);
            bodyTransform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(bodyTransform.rotation.eulerAngles.y, getClosestAngle(bodyTransform.rotation.eulerAngles.y, angle), Mathf.Abs(vertValue)), 0);
        }
        else if (Mathf.Abs(horzValue) > 0)
        {
            //print(angle);
            bodyTransform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(bodyTransform.rotation.eulerAngles.y, getClosestAngle(bodyTransform.rotation.eulerAngles.y, angle), Mathf.Abs(horzValue)), 0);
        }
    }

    //this is called when ever the tank is damaged
    public void damage(float damage, GameObject other)
    {
        health -= damage;
        print("Health is: " + health);
    }

    //this gets the closest angle to the angle we want so the tank doesnt rotate all the way around 
    private float getClosestAngle(float currentAngle, float wantedAngle)
    {
        if (wantedAngle >= 180)
        {
            if (currentAngle - wantedAngle > currentAngle - (wantedAngle - 180))
            {
                return wantedAngle;
            }
            else
            {
                return wantedAngle - 180;
            }
        }
        else
        {
            if (currentAngle - wantedAngle > currentAngle - (wantedAngle + 180))
            {
                return wantedAngle;
            }
            else
            {
                return wantedAngle + 180;
            }
        }


    }

}
