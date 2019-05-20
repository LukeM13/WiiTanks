using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public void GoBack()
    {
        print("checking");
        GameObject main = GameObject.Find("Main");
        main.SetActive(true);
        //GameObject op = GameObject.Find("Options");
        //op.SetActive(false);
        //GameObject help = GameObject.Find("Instructions");
        //help.SetActive(false);
    }
}
