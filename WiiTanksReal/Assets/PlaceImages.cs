using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaceImages : MonoBehaviour
{
    public GameObject icon;
    private VerticalLayoutGroup layout;
    private BulletManager bulletManager;
    // Start is called before the first frame update
    void Start()
    {
        layout = GetComponent<VerticalLayoutGroup>();
        bulletManager = GameObject.FindGameObjectWithTag("Player").GetComponent<BulletManager>();
        foreach(BulletParent b in bulletManager.bullets)
        {
            GameObject obj = (GameObject)Instantiate(icon);
            obj.transform.SetParent(layout.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
