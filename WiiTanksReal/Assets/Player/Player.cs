﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Damageable
{
    public float health;
    public ParticleSystem deathParticle;

    public float speed;
    private BulletManager bulletManager;

    private CharacterController controller;
    private Rigidbody rigid;

    public Transform bodyTransform;
    private float vertValue = 0.0f;
    private float horzValue = 0.0f;
    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private SceneController sceneController;

    private float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        //this gets the character controller that is attached to this object
        controller = GetComponent<CharacterController>();
        //this gets the rigid body from the object
        rigid = GetComponent<Rigidbody>();
        //This is what spawns the bullets and keeps track of bullet data
        bulletManager = GetComponent<BulletManager>();

        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the value for moving up and down
        horzValue = Input.GetAxis("Horizontal");
        //set the value for moving left and right
        vertValue = Input.GetAxis("Vertical");

        if (bulletManager.shouldMove) {
            moveTank();
            turnTank();
        }


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
        rigid.MovePosition(transform.position + (moveDir * Time.deltaTime * speed));
        //controller.Move();
    }
    bool goingUp = false;
    bool goingRight = false;
    //turns tank
    void turnTank()
    {


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            goingUp = true;
        } else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            goingUp = false;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            goingRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            goingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            goingUp = true;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            goingUp = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            goingRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            goingRight = false;
        }
        if (goingUp || goingRight)
        {
            bodyTransform.rotation = Quaternion.LookRotation(moveDir * Time.deltaTime * speed);
        }
    }


    //this is called when ever the tank is damaged
    public void damage(float damage, GameObject other)
    {
        health -= damage;
        print("Health is: " + health);
        if (health <= 0)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
            deathParticle.Play();
            sceneController.playerKill();
            Destroy(gameObject);
        }
    }

    //this gets the closest angle to the angle we want so the tank doesnt rotate all the way around 
    private float getClosestAngle(float currentAngle, float wantedAngle)
    {

        if (wantedAngle >= 180)
        {
            if (wantedAngle - currentAngle < (wantedAngle - 180) - currentAngle)
            {
               // print("123232Closer angle to " + (wantedAngle - currentAngle) + "is: " + ((wantedAngle - 180) - currentAngle));
                return wantedAngle;
            }
            else if (wantedAngle - currentAngle == (wantedAngle - 180) - currentAngle)
            {
                return currentAngle;
            }
            else
            {
                //print("2Closer angle to " + currentAngle + "is: " + (wantedAngle - 180));
                return wantedAngle - 180;
            }
        }
        else
        {
            if (wantedAngle - currentAngle >= (wantedAngle + 180) - currentAngle)
            {
                //print("3Closer angle to " + currentAngle + "is: " + wantedAngle);
                return wantedAngle;
            }
            else
            {
                //print("4Closer angle to " + currentAngle + "is: " +(wantedAngle + 180));
                return wantedAngle + 180;
            }
        }


    }

}
