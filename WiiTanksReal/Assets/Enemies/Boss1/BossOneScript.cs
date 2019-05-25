using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOneScript : AIParent
{

    public float followSpeed;

    public GameObject turret;

    private int activePlayer;

    public float shootSpeed;

    private float shootTimer;

    public Object bulletType;

    public Object sideBullets;

    private float defaultHealth;

    private Slider healthBar;

    [SerializeField]
    public GameObject[] spawnPoints;

    public GameObject midSpawnPoint;

    private Canvas canvas;

    public void Start()
    {
        base.Start();
        defaultHealth = health;

        healthBar = GetComponentInChildren<Slider>();

        canvas = GetComponentInChildren<Canvas>();
    }


    // Update is called once per frame
    void Update()
    {

        navAgent.SetDestination(getClosestPlayer().transform.position);
        turret.transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(getClosestPlayer().transform.position.x, transform.position.y, getClosestPlayer().transform.position.z));
        if (canSeePlayer(getClosestPlayer()))
        {
            if (shootTimer >= shootSpeed)
            {
                print("fireBullet");
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(sideBullets, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                }
                Instantiate(bulletType, midSpawnPoint.transform.position, midSpawnPoint.transform.rotation);
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
        canvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);
        
    }
    //we we are hit with a bullet
    public override void damage(float damage, GameObject other)
    {
        health -= damage;

        healthBar.value = health / defaultHealth;

        if (health <= 0)
        {
            death();

        }
    }
}
