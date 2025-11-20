using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentEventSystem : MonoBehaviour
{
    private static PersistentEventSystem instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}