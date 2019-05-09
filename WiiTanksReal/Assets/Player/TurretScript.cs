using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Vector2 mousePos;
    private Vector3 screenPos;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        screenPos = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - camera.transform.position.z));

        RaycastHit hit;

        if(Physics.Raycast(camera.transform.position, screenPos - camera.transform.position, out hit, 1000))
        {
            transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(hit.point.x, transform.position.y, hit.point.z));

        }
    }
}
