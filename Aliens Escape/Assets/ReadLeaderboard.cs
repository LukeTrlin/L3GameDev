using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadLeaderboard : MonoBehaviour
{
    public TextMeshProUGUI targetText; // TMP text field
    public string fileName = "Leaderboard.txt"; // file in Assets/TextFiles or StreamingAssets
    public int countToShow = 10;

    bool done;

    void LateUpdate()
    {
        if (done) return;

        if (targetText == null)
        {
            targetText = GetComponent<TextMeshProUGUI>() ?? FindObjectOfType<TextMeshProUGUI>();
            if (targetText == null)
            {
        
                return;
            }
        }

        // Locate the file
        string path = Path.Combine(Application.dataPath, "TextFiles", fileName);
        if (!File.Exists(path))
            path = Path.Combine(Application.streamingAssetsPath, "TextFiles", fileName);
        if (!File.Exists(path))
        {
           
            return;
        }

        string[] lines = File.ReadAllLines(path);
        var entries = new List<(string name, float time)>();

        foreach (var raw in lines)
        {
            if (string.IsNullOrWhiteSpace(raw)) continue;

            // Split CSV
            string[] parts = raw.Split(',');
            if (parts.Length != 2)
            {
   
                continue;
            }

            string name = parts[0].Trim();
            string timeStr = parts[1].Trim();

            if (!float.TryParse(timeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float time))
            {
               
                continue;
            }

            entries.Add((name, time));
        }

        if (entries.Count == 0)
        {
         
            return;
        }

        // Sort by lowest time and take top N
        var sorted = entries.OrderBy(e => e.time).Take(countToShow).ToList();

        // Build display lines (simple name + space + time)
        List<string> formatted = new List<string>();
        foreach (var entry in sorted)
        {
            formatted.Add(entry.name + " " + entry.time.ToString("0.###", CultureInfo.InvariantCulture));
        }

        targetText.text = string.Join("\n", formatted);

        done = true;
    }
}