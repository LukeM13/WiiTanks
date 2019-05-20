using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletPrefabScript : MonoBehaviour
{
    public BulletParent bullet;

    private Button button;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.image = bullet.icon;

        text = GetComponentInChildren<TMP_Text>();
        text.text = bullet.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
