using System.IO;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UploadScore : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statusText;  
    public string playerName;

    private GlobalVariables globalVariables;

    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();

        if (nameText == null)
        {
            Debug.LogWarning("UploadScore: nameText reference is not assigned.");
            return;
        }

        UpdatePlayerName();
    }

    void Update()
    {
        if (nameText != null)
            UpdatePlayerName();
    }

    private void UpdatePlayerName()
    {
        string cleanedName = CleanName(nameText.text);
        playerName = cleanedName;

        if (globalVariables != null)
            globalVariables.PlayerName = cleanedName;
    }

    // ------------------------------------------
    // CLEAN STRING HELPER
    // ------------------------------------------
    private string CleanName(string input)
    {
        return (input ?? string.Empty)
            .Replace("\u200B", "")   // zero-width space
            .Replace("\u00A0", "")   // non-breaking space
            .Trim();
    }

    // ------------------------------------------
    // UPLOAD SCORE
    // ------------------------------------------
    public void UploadScoreToLeaderboard()
    {
        string finalPlayerName = CleanName(nameText.text);

        // -------------------------
        // VALIDATION: EMPTY NAME
        // -------------------------
        if (string.IsNullOrEmpty(finalPlayerName))
        {
            Debug.LogWarning("Cannot upload score: name is empty.");
            SetStatus("❌ Name cannot be empty!");
            return;
        }

        float timeValue = globalVariables != null ? globalVariables.TimeTaken : 0f;

        // -------------------------
        // PICK FILE BY DIFFICULTY
        // -------------------------
        string difficultyFile = GetDifficultyFile();

        string dir = Path.Combine(Application.dataPath, "TextFiles");
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string path = Path.Combine(dir, difficultyFile);

        try
        {
            string line = $"{finalPlayerName},{timeValue:F2}";
            File.AppendAllText(path, line + System.Environment.NewLine);

            Debug.Log($"Saved {line} to {path}");
            SetStatus("✅ Score uploaded!");

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to write leaderboard file: {ex}");
            SetStatus("❌ Upload failed! Check console.");
        }
    }

    // ------------------------------------------
    // SELECT FILE FOR DIFFICULTY
    // ------------------------------------------
    private string GetDifficultyFile()
    {
        if (globalVariables == null)
        {
            Debug.LogWarning("GlobalVariables not found!");
            return "Leaderboard_Unknown.txt";
        }

        switch (globalVariables.difficultyLevel)
        {
            case 1: return "Leaderboard_Easy.txt";
            case 2: return "Leaderboard_Medium.txt";
            case 3: return "Leaderboard_Hard.txt";
            default:
                Debug.LogWarning("Invalid difficulty level!");
                return "Leaderboard_Unknown.txt";
        }
    }

    // ------------------------------------------
    // UPDATE UI STATUS TEXT
    // ------------------------------------------
    private void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }
}