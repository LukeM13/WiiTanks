using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [HideInInspector]
    public List<BulletParent> bullets;

    public Transform spawnPoint;
    public BulletParent testBullet;
    private ParticleSystem turretSmoke;

    public int activeBullet = 0;

    private void Start()
    {
        //this gets the turret smoke particle system on the turret object
        turretSmoke = GetComponentInChildren<ParticleSystem>();

        bullets.Add(testBullet);
    }

    void Update()
    {
        //this shoots the bullet
        if (Input.GetButtonDown("Fire1"))
        {

            Instantiate(bullets[activeBullet], spawnPoint.position, spawnPoint.rotation);
            //play particle effect
            turretSmoke.Play();
        }

    }
}
