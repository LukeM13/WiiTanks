using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Smart_AI : AIParent
{
    [Header("Aiming")]
    //the transform of the turret
    public Transform turretTransform;
    //how fast the turret should snap to the player
    public float followSpeed;
    //the number of bullets that should be shot in one burst
    public int burstAmount;
    //this index of the player we can see
    private int activeIndex = -1;
    //the timer for moving the turret
    private float smoothTurret = 0;
    //the time between each burst of bullets
    public float shootTime;
    //the timer for the bursts
    private float currentShootTime;
    [Header("Shooting")]
    //the bullet that this tank shoot
    public Object bullet;
    //the spawn point for the bullets
    public Transform spawnPoint;

    private Player playerMovement;

    [Header("Brains Stuff")]
    [Range(1,20)]
    public float minDistFromPlayer;
    public int RaysToShoot;

    private Vector3 aimingAngle;

    private GameObject[] boundry;

    public void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Player>();

        boundry = GameObject.FindGameObjectsWithTag("Boundry");

    }

    // Update is called once per frame
    void Update()
    { 


        if (canSeePlayer(player))
        {
            aimingAngle = (player.transform.position + (playerMovement.moveDir * 2)) - transform.position;
        }
        else
        {
            aimingAngle = bestAngle();
        }

        turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, Quaternion.LookRotation(aimingAngle), currentShootTime/shootTime);
        if (smoothTurret < 1)
        {
            smoothTurret += Time.deltaTime * followSpeed;
        }

        if (currentShootTime >= shootTime)
        {

            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            currentShootTime = 0;

        }
        else
        {
            currentShootTime += Time.deltaTime;
        }
    }

    private Vector3 bestAngle()
    {
        Vector3 angle = transform.forward;
        float minDist = float.MaxValue;
        for (int i = 0; i < RaysToShoot; i++)
        {
            Vector3 dir = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + ((360 / RaysToShoot) * i),
                transform.rotation.z) * transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit))
            {
                
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    angle = dir;
                    smoothTurret = 0;
                    break;
                }
                if (hit.collider.gameObject.tag.Equals("Wall") || hit.collider.gameObject.tag.Equals("Boundry"))
                {

                    RaycastHit playerCheck;
                    if (Physics.Raycast(hit.point, player.transform.position - hit.point, out playerCheck))
                    {
                        Debug.DrawLine(hit.point, playerCheck.point, Color.red);
                        if (!playerCheck.collider.gameObject.tag.Equals("Wall"))
                        {
                            if (Vector3.Distance(hit.point, player.transform.position) < minDist)
                            {
                                print("draw line");
                                
                                angle = dir;
                                smoothTurret = 0;
                                minDist = Vector3.Distance(hit.point, player.transform.position);

                            }
                        }

                    }
    
                    RaycastHit reflectHit;
                    Vector3 reflectDir = Vector3.Reflect(dir, hit.normal);
                    if (Physics.Raycast(hit.point, reflectDir, out reflectHit))
                    {
                        if (Vector3.Distance(reflectHit.point, player.transform.position) < minDist)
                        {
                            angle = dir;
                            smoothTurret = 0;
                            minDist = Vector3.Distance(reflectHit.point, player.transform.position);
                        }
                        //Debug.DrawLine(hit.point, reflectHit.point, Color.black);
                    }
                }

            }
        }
        return angle;
    }
}
