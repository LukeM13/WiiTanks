using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletParent : MonoBehaviour
{
    [Header("UI Stuff For Bullet Select")]
    public Sprite icon;
    public string bulletName;

    [Header("Values to Affect the Bullet")]
    [Range(1, 50)]
    public float speed = 1.0f;
    public float damage;
    public int maxNumberOfWallHits = 1;
    public int numOfHits;
    public ParticleSystem deathParticle;
    protected bool destroy;


    protected bool stillInWall = false;
    //this is called every time the collider hits an object with a collider
    protected void OnCollisionEnter(Collision col)
    {
        if (!stillInWall) {
            //checks to see if we hit a wall
            if (col.gameObject.tag.Equals("Wall"))
            {

                //this gets the reflected vector and sets the forward direction of this object to that vector
                transform.forward = Vector3.Reflect(transform.forward, col.GetContact(0).normal);
                numOfHits++;
                stillInWall = true;
            }
            else if (col.gameObject.tag.Equals("Boundry"))
            {
                //this gets the reflected vector and sets the forward direction of this object to that vector
                transform.forward = Vector3.Reflect(transform.forward, col.GetContact(0).normal);
                numOfHits++;
                stillInWall = true;
            }
            //checks to see if we hit a tank
            else if (col.gameObject.tag.Equals("Tank"))
            {
                //same thing as the wall
                List<Damageable> damageScripts;
                GlobalFunctions.GetInterfaces<Damageable>(out damageScripts, col.gameObject);
                foreach (Damageable damageObject in damageScripts)
                {
                    destroy = true;
                    damageObject.damage(damage, this.gameObject);
                }

            }
            else if (col.gameObject.tag.Equals("Player"))
            {
                //same thing as the wall
                List<Damageable> damageScripts;
                GlobalFunctions.GetInterfaces<Damageable>(out damageScripts, col.gameObject);
                foreach (Damageable damageObject in damageScripts)
                {
                    destroy = true;
                    damageObject.damage(damage, this.gameObject);
                }
            }
            else if (col.gameObject.tag.Equals("Bullet"))
            {
                destroy = true;
            }
            
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        stillInWall = false;
    }
}
