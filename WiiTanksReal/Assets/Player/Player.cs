using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public Transform spawnPoint;
    public Object bulletType;

    private CharacterController controller;
    private Rigidbody rigid;
    private Vector3 moveDir = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletType, spawnPoint.position, spawnPoint.rotation);
        }

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir = transform.TransformDirection(moveDir);
        moveDir *= speed;
        controller.Move(moveDir * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Going up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Going down");
        }
        {

        }
    }
}
