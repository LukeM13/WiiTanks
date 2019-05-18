using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneScript : AIParent, Damageable
{

    public float followSpeed;

    public GameObject turret;

    private int activePlayer;

    public float shootSpeed;

    private float shootTimer;

    public Object bulletType;

    [SerializeField]
    public GameObject[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        turret.transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(getClosestPlayer().transform.position.x, transform.position.y, getClosestPlayer().transform.position.z));
        if (canSeePlayer(getClosestPlayer()))
        {
            if (shootTimer >= shootSpeed)
            {
                print("fireBullet");
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(bulletType, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                }
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }


    }

    public void damage(float damage, GameObject other)
    {
        
    }
}
