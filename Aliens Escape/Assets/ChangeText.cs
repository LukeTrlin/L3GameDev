using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TextMeshProUGUI Timetext;
    public TextMeshProUGUI Killstext;

     private GlobalVariables globalVariables;

    void Start()
    {
         globalVariables = FindObjectOfType<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        Timetext.text = "Time: " + globalVariables.TimeTaken.ToString("F2") + " s";
        Killstext.text = "Kills: " + globalVariables.enemyKills;
    }
}
