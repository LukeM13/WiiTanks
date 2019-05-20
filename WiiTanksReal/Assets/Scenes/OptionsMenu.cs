using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void openOptions()
    {
        GameObject main = GameObject.Find("Main");
        main.SetActive(false);
        GameObject op = GameObject.Find("Options");
        op.SetActive(true);
        GameObject help = GameObject.Find("Instructions");
        help.SetActive(false);
    }
}
