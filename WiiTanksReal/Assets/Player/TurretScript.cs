using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Vector2 mousePos;
    private Vector3 screenPos;

    private Camera camera;


    private LineRenderer mouseLine;

    // Start is called before the first frame update
    void Start()
    {
        //this gets the scenes main camera
        camera = Camera.main;

        mouseLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //this gets the mouse location on the screen which is a pixel location
        mousePos = Input.mousePosition;
        //this turns that pixel location into a point on the world
        screenPos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - camera.transform.position.z));

        //this is the output for the raycast
        RaycastHit hit;
        //this sends a raycast from the location of the camera to the screenPos so that the turret only rotates
        //on one axis and not 3
        if (Physics.Raycast(camera.transform.position, screenPos - camera.transform.position, out hit, 1000))
        {
            //this is called if the raycast his something and it turns the turret toward
            //the that hit point
            transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(hit.point.x, transform.position.y, hit.point.z));
            /*
            RaycastHit lineHit;
            
            if (Physics.Raycast(transform.position, (transform.position - new Vector3(hit.point.x, transform.position.y, hit.point.z)) * -1, out lineHit, 1000))
            {
                //if (lineHit.collider.gameObject.tag.Equals("Boundry"))
                //{
                //    mouseLine.enabled = true;
                //    mouseLine.SetPosition(0, transform.position);
                //    mouseLine.SetPosition(1, new Vector3(hit.point.x, transform.position.y, hit.point.z));
                //}
                //else
                //{
                //    mouseLine.enabled = false;
                //}

            }
            */
        }
    }
}
