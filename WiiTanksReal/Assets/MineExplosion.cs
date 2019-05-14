using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    private SphereCollider collider;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
