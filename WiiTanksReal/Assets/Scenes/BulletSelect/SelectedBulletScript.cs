using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedBulletScript : MonoBehaviour
{

    private GameObject SceneController;

    [HideInInspector]
    public Image icon;

    private TMP_Text bulletName;

    public int index;

    [HideInInspector]
    public BulletParent bulletObj;

    // Start is called before the first frame update
    void Start()
    {
        SceneController = GameObject.FindGameObjectWithTag("SceneController");

        icon = GetComponent<Image>();

        bulletName = GetComponentInChildren<TMP_Text>();
    }

    public void updateValues(BulletParent bulletVal)
    {
        icon.sprite = bulletVal.icon;

        bulletName.text = bulletVal.name;

        icon.color = Color.white;

        bulletObj = bulletVal;

    }
}
