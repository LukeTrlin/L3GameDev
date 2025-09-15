using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject triggerPlatform;


    private GlobalVariables globalVariables;

    private bool measureTime = true;

    // Start is called before the first frame update
    void Awake()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        measureTime = true;
        StartCoroutine("Timer");
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            measureTime = false;
            Debug.Log("End screen triggered");
            ShowEndScreen();
        }
    }

    private void ShowEndScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
        // Implement your logic to show the end screen
    }

    public IEnumerator Timer()
    {
        float lastTime = Time.time;
        while (measureTime)
        {
            yield return null;
            float currentTime = Time.time;
            globalVariables.TimeTaken += currentTime - lastTime;
            lastTime = currentTime;
        }
    }
}
