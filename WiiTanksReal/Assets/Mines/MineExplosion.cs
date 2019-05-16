using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    private SphereCollider collider;
    public float damage;
    public float lifetime;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= lifetime)
        {
            Destroy(gameObject);
        }
        currentTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("Tank"))
        {
            List<Damageable> tanks;

            GlobalFunctions.GetInterfaces<Damageable>(out tanks, col.gameObject);

            foreach (Damageable tank in tanks)
            {
                tank.damage(damage, this.gameObject);
            }
        }
    }
}
