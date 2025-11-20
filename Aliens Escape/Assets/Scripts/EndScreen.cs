using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement; // Needed for scene loading
public class EndScreen : MonoBehaviour
{
    public static EndScreen instance;

    private GlobalVariables globalVariables;
    private bool measureTime = false;
    private Coroutine timerCoroutine;

    private void Awake()
    {
        // Singleton pattern: prevent duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        

        globalVariables = FindObjectOfType<GlobalVariables>();

        if (globalVariables.TimeTaken <= 0f)
            globalVariables.TimeTaken = 0f;

        StartTimer();
    }

    private void StartTimer()
    {
        if (timerCoroutine == null)
        {
            measureTime = true;
            timerCoroutine = StartCoroutine(Timer());
        }
    }

    private void StopTimer()
    {
        measureTime = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopTimer();
            
            ShowEndScreen();
        }
    }

   
    private void ShowEndScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("EndgameScene");
    }

    private IEnumerator Timer()
    {
        while (measureTime)
        {
            globalVariables.TimeTaken += Time.deltaTime;
            yield return null;
        }
    }

    public void ResetAndStartTimer()
{
    StopTimer(); // stop any old timer just in case
    globalVariables.TimeTaken = 0f;
    StartTimer();
}

    
}