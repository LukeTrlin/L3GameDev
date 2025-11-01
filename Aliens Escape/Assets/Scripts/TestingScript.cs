using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestingScript : MonoBehaviour
{
    // relative project path (used by AssetDatabase)
    string relativePath = "Assets/TextFiles/Leaderboard.txt";
    // full OS path used for file IO
    string fullPath;

    void Awake()
    {
        fullPath = Path.Combine(Application.dataPath, "TextFiles", "Leaderboard.txt");
        string dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    void Start()
    {
        // Append "test" and a newline to the file
        File.AppendAllText(fullPath, "test" + System.Environment.NewLine);

        // Refresh the Editor so the updated file is visible in the Project window
    #if UNITY_EDITOR
        AssetDatabase.ImportAsset(relativePath);
    #endif
    }
}
