using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //the tank is the player we are trying to teleport
    //public GameObject tank;
    public Transform teleportTo;
    private Vector3 startPos;
    Collider collide;
    private int i = 0;
    private Vector3 randPos = new Vector3(180, 11, 10);
    // Start is called before the first frame update
    void Start()
    {
        collide = GetComponent<Collider>();

       // startPos = .transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(tank.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
       
        if (Input.GetKeyDown(KeyCode.E) && collide.gameObject.tag.Equals("Player")) {
            if (i%2 == 0)
            {
                Debug.Log("Before:" + collide.gameObject.transform.position);
                // this line should be good enough to transport the tank
                collide.gameObject.transform.position = teleportTo.position;
                Debug.Log("Target:" + teleportTo.position);
                Debug.Log("After:" + collide.gameObject.transform.position);
                CheckForMove();
                //Debug.Log(other.name);

            }
            i++;
        }
       
    }

    private void CheckForMove()
    {
       //Instantiate(tank,tank.transform);
       //to check if the position of the tank actually changed
    }
}
