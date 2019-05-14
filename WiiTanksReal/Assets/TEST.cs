using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TEST 1");
        Instantiate(test, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
