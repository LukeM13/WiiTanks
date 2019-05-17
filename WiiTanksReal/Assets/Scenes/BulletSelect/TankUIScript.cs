using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TankUIScript : MonoBehaviour
{

    private TankType type;

    private string count;

    public GameObject nameObject;
    private TMP_Text name;
    public GameObject tankCountObject;
    private TMP_Text tankCount;


    private void Start()
    {
        

    }

    public void updateValues(TankType tt, int num)
    {
        name = nameObject.GetComponent<TMP_Text>();
        tankCount = tankCountObject.GetComponent<TMP_Text>();
        type = tt;
        count = "x " + num;
        name.SetText(tt.ToString());
        tankCount.SetText(count);
    }
}
