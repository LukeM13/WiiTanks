using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    Rigidbody rigid;
    Collider collider;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        //rigid.AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Wall")
        {
            transform.forward = Vector3.Reflect(transform.forward, col.GetContact(0).normal);
        } else if (col.gameObject.tag == "Tank")
        {

        }
    }
}
