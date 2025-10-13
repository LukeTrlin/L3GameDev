using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI Timetext;
    public TextMeshProUGUI Killstext;
    public TextMeshProUGUI Ammotext;

     private GlobalVariables globalVariables;

    void Start()
    {
         globalVariables = FindObjectOfType<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (globalVariables != null)
        {
            if (Timetext != null)
                Timetext.text = "Time: " + globalVariables.TimeTaken.ToString("F2") + " s";
            if (Killstext != null)
                Killstext.text = "Kills: " + globalVariables.enemyKills;
            if (Ammotext != null)
                Ammotext.text = "Ammo: " + globalVariables.currentAmmo;
        }
    }
}
