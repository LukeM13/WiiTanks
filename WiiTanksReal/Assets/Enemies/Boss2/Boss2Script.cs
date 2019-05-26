using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Script : MonoBehaviour, Damageable
{

    public GameObject[] spawnPoint;

    public GameObject[] tanksToSpawn;

    public float waveTime;

    private float waveTimer;

    public float health;

    private float maxHealth;

    private Slider healthBar;

    [SerializeField]
    protected ParticleSystem deathParticle;

    private Canvas canvas;

    private static SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;

        healthBar = GetComponentInChildren<Slider>();

        canvas = GetComponentInChildren<Canvas>();

        spawnWave();
    }

    // Update is called once per frame
    void Update()
    {

        if (waveTimer >= waveTime)
        {
            spawnWave();
            waveTimer = 0;
        }
        else
        {
            waveTimer += Time.deltaTime;
        }
        canvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);
    }

    private void spawnWave()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            Instantiate(tanksToSpawn[Random.Range(0, tanksToSpawn.Length)], spawnPoint[i].transform.position, spawnPoint[i].transform.rotation);
        }
    }

    public void damage(float damage, GameObject other)
    {
        health -= damage;

        healthBar.value = health / maxHealth;

        if (health <= 0)
        {
            death();

        }
    }

    protected void death()
    {
        //makes a particle effect that hides the tank disappearing
        Instantiate(deathParticle, transform.position, transform.rotation);
        deathParticle.Play();
        print(sceneController);
        Destroy(gameObject);

    }
}
