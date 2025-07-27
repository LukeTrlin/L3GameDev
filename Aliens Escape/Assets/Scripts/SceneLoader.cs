using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadScene(int sceneIndex)
    {
        // Load the scene with the specified index
        
        SceneManager.LoadScene(sceneIndex);
    }


    public void ReturnToMainMenu()
    {
        // Load the main menu scene (assuming index 0 is the main menu)
       


        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void CloseOptions()
    {
        
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.transform.parent.gameObject.SetActive(false); // Deactivate the options panel
        Debug.Log(gameObject.transform.parent.gameObject.name + " has been deactivated.");
        Time.timeScale = 1;
    }
}
