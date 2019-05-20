using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletPrefabScript : MonoBehaviour
{
    public BulletParent bullet;

    private Button button;
    public Image bulletIcon;
    private TMP_Text bulletName;

    // Start is called before the first frame update
    void Start()
    {
        bulletIcon.sprite = bullet.icon;

        bulletName = GetComponentInChildren<TMP_Text>();
        bulletName.text = bullet.name;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
