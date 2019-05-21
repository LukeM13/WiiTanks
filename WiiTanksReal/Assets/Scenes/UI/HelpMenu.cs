using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpMenu : MonoBehaviour
{
    private GameObject help;
    private GameObject menu;
    public void OpenHelp()
    {
        help = GameObject.Find("Instructions");
        help.SetActive(true);
        menu = GameObject.Find("Main");
        menu.SetActive(false);
        GameObject op = GameObject.Find("Options");
        op.SetActive(false);
    }
}
