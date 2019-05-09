using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour, Damageable
{

    //tests in a bullet is applying damage
    public void damage(float damage, GameObject other)
    {
        print("Damage: " + damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
