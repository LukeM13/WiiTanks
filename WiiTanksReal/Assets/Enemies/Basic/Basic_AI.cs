using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Basic_AI : MonoBehaviour, Damageable
{

    private NavMeshAgent navAgent;
    public ParticleSystem deathParticle;

    public float health;


    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = navAgent.remainingDistance;
        if (dist != Mathf.Infinity && navAgent.pathStatus == NavMeshPathStatus.PathComplete && navAgent.remainingDistance == 0)
        {

            Vector3 randomDirection = Random.insideUnitSphere * 40;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 40, 1);
            Vector3 finalPosition = hit.position;
            navAgent.SetDestination(finalPosition);
            print("finding new path");
        }
    }

    public void damage(float damage, GameObject other)
    {
        health -= damage;
        print(health);
        if (health <= 0)
        {
            print("Dead!");
            Instantiate(deathParticle, transform.position, transform.rotation);
            
            //deathParticle.transform.position = transform.position;
            deathParticle.Play();
            Destroy(gameObject);
        }
    }
}
