using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenScoreCalculation : MonoBehaviour
{
    public TextMeshProUGUI timeTextGrade;

    private GlobalVariables globalVariables;

    void Awake()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
       

    }


    void Update()
    {
        if (globalVariables.TimeTaken <= 10)
        {
            timeTextGrade.text = "Time Efficiency: " + "P";
        }
        else if (globalVariables.TimeTaken <= 20)
        {
            timeTextGrade.text = "Time Efficiency: " + "A";

        }
        else if (globalVariables.TimeTaken <= 30)
        {
            timeTextGrade.text = "Time Efficiency: " + "B";
        }
        else if (globalVariables.TimeTaken <= 40) 
        {
            timeTextGrade.text = "Time Efficiency: " + "C";
        }
    }
}

