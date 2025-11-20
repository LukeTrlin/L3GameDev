using UnityEngine;

public class PersistentOptionsMenu : MonoBehaviour
{
    public static PersistentOptionsMenu Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false); // start hidden
    }
}