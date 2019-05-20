using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public Object explosion;
    private bool isSpawnedInPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (((col.gameObject.tag.Equals("Tank") || col.gameObject.tag.Equals("Player")) && !isSpawnedInPlayer) || col.gameObject.tag.Equals("Bullet"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag.Equals("Tank") || col.gameObject.tag.Equals("Player")) && isSpawnedInPlayer)
        {
            isSpawnedInPlayer = false;
           
        }
    }
}
