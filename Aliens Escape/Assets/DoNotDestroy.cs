using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public static DoNotDestroy Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this; // 
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false); // optional, start hidden
    }
}