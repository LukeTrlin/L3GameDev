using System.IO;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UploadScore : MonoBehaviour
{
    public TextMeshProUGUI nameText;
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
        {
            UpdatePlayerName();
        }
    }

    private void UpdatePlayerName()
    {
        // Clean up invisible or non-breaking characters
        string cleanedName = (nameText.text ?? string.Empty)
            .Replace("\u200B", "")   // zero-width space
            .Replace("\u00A0", "")   // non-breaking space
            .Trim();

        playerName = cleanedName;

        if (globalVariables != null)
            globalVariables.PlayerName = cleanedName;

        Debug.Log($"Raw nameText.text='{nameText.text}' (Length={nameText.text.Length}) | Cleaned='{cleanedName}'");
    }

    public void UploadScoreToLeaderboard()
    {
        if (nameText == null)
        {
            Debug.LogWarning("Cannot upload score: nameText reference is not assigned.");
            return;
        }

        // Clean again before saving
        string finalPlayerName = (nameText.text ?? string.Empty)
            .Replace("\u200B", "")
            .Replace("\u00A0", "")
            .Trim();

        if (string.IsNullOrEmpty(finalPlayerName))
        {
            Debug.LogWarning("Cannot upload score: player name is empty or invalid.");
            return;
        }

        float timeValue = globalVariables != null ? globalVariables.TimeTaken : 0f;
        if (globalVariables == null)
            Debug.LogWarning("GlobalVariables not found. TimeTaken is assumed 0.");

        string dir = Path.Combine(Application.dataPath, "TextFiles");
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string path = Path.Combine(dir, "Leaderboard.txt");

        try
        {
            string line = $"{finalPlayerName} {timeValue:F2}";
            File.AppendAllText(path, line + System.Environment.NewLine);
            Debug.Log($"Leaderboard: saved {finalPlayerName} {timeValue:F2} to {path}");

    #if UNITY_EDITOR
            AssetDatabase.Refresh();
    #endif
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to write leaderboard file: {ex}");
        }
    }
}