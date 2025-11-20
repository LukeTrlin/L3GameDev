using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenScoreCalculation : MonoBehaviour
{

    public TextMeshProUGUI timeTextGrade;

    private GlobalVariables globalVariables;

    private int timeScore;

        public TextMeshProUGUI multiplierText; // NEW: Text to display multiplier


    public float maxTimeScore = 150f;      // Maximum points for time
    public float pointsPerKill = 10f;      // Points per enemy killed
    public float difficultyMultiplier = 1f; // Default multiplier if not using GlobalVariables

    public TextMeshProUGUI enemiesTextGrade;
    public TextMeshProUGUI finalTextGrade;
    private void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        CalculateAndDisplayScore();
    }
    void Awake()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
       CalculateAndDisplayScore();

    }

     void CalculateAndDisplayScore()
    {
        // --- TIME RANK ---
        float time = globalVariables.TimeTaken;
        if (time <= 90f)
            timeTextGrade.text = "Time Efficiency: P";
        else if (time <= 110f)
            timeTextGrade.text = "Time Efficiency: A";
        else if (time <= 130f)
            timeTextGrade.text = "Time Efficiency: B";
        else
            timeTextGrade.text = "Time Efficiency: C";

        // --- ENEMY RANK ---
        int effectiveKills = globalVariables.enemyKills - globalVariables.TimesDied;
        effectiveKills = Mathf.Max(0, effectiveKills); // Prevent negative

        if (effectiveKills >= 10)
            enemiesTextGrade.text = "Combat Efficiency: P";
        else if (effectiveKills >= 8)
            enemiesTextGrade.text = "Combat Efficiency: A";
        else if (effectiveKills >= 5)
            enemiesTextGrade.text = "Combat Efficiency: B";
        else
            enemiesTextGrade.text = "Combat Efficiency: C";

        // --- CALCULATE FINAL SCORE ---
        float timeScore = Mathf.Max(0, maxTimeScore - time); // Faster = more points
        float killScore = effectiveKills * pointsPerKill;

        float multiplier = globalVariables.difficultyLevel;

        float finalScore = (timeScore + killScore) * multiplier;
        
        if (multiplierText != null)
            multiplierText.text = "Multiplier " + multiplier.ToString("0.0") + " x";

        // --- FINAL RANK ---
        string finalRank;
        if (finalScore >= 400) finalRank = "P";
        else if (finalScore >= 300) finalRank = "A";
        else if (finalScore >= 200) finalRank = "B";
        else finalRank = "C";

        finalTextGrade.text = "Final Rank: " + finalRank;
    }
}

 

