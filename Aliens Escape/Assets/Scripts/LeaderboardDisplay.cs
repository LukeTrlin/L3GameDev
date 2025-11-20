using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardDisplay : MonoBehaviour
{
    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public float time;

        public ScoreEntry(string playerName, float time)
        {
            this.playerName = playerName;
            this.time = time;
        }
    }

    public Transform contentParent;  // Parent object with a VerticalLayoutGroup
    public GameObject scorePrefab;   // Prefab with a TMP_Text component

    private List<ScoreEntry> leaderboard = new List<ScoreEntry>();

    void Start()
    {
        LoadLeaderboard();
        DisplayLeaderboard();
    }

    void LoadLeaderboard()
    {
        string path = Path.Combine(Application.dataPath, "TextFiles", "Leaderboard.txt");

        if (!File.Exists(path))
        {
      
            return;
        }

        leaderboard.Clear();
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length >= 2 && float.TryParse(parts[1], out float time))
            {
                leaderboard.Add(new ScoreEntry(parts[0], time));
            }
        }

        // Sort ascending (smallest time = best)
        leaderboard.Sort((a, b) => a.time.CompareTo(b.time));
    }

    void DisplayLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Display all entries
        int rank = 1;
        foreach (ScoreEntry entry in leaderboard)
        {
            GameObject newEntry = Instantiate(scorePrefab, contentParent);
            TMP_Text text = newEntry.GetComponent<TMP_Text>();
            text.text = $"{rank}. {entry.playerName} - {entry.time:F2}s";
            rank++;
        }
    }
}