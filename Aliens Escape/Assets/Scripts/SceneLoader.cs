using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
     private GlobalVariables globalVariables;

    public GameObject OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();

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

    public void StartGame()
    {
        EndScreen.instance.ResetAndStartTimer();
        SceneManager.LoadScene(1);
        
    }

    public void ReturnToMainMenu()
    {
        // Load the main menu scene (assuming index 0 is the main menu)
       
        globalVariables.isOptionsOpen = false;
        Cursor.lockState = CursorLockMode.None; // Set lock state to None
        Cursor.visible = true; // Make the cursor visible
        Time.timeScale = 1;
       resetVariables();
        SceneManager.LoadScene(0);
    }

    public void CloseOptionsFromTitle()
    {
        globalVariables.isOptionsOpen = false;
        OptionsMenu.SetActive(false);
        
    }
    public void CloseOptions()
    {
        globalVariables.isOptionsOpen = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; // Set lock state to None
        Cursor.visible = false; // Make the cursor visible
        OptionsMenu.SetActive(false);
        
    }

    public void EndScreenLoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        resetVariables();
    }

    public void resetVariables()
    {

        globalVariables.difficultyLevel = 0; // 0: Easy, 1: Medium, 2: Hard

        globalVariables.playerScore = 0;
        globalVariables.playerHealth = 100; // Default health value

        globalVariables.currentHealth = 100;



        globalVariables.enemyHealth = 50;

        globalVariables.TimeTaken = 0.0f;

        globalVariables.enemyKills = 0;

        globalVariables.TimesDied = 0;
        globalVariables.currentAmmo = 30; // Default ammo count
        globalVariables.playerMaxAmmo = 90; // Default max ammo count

    }


    public void QuitGame()
    {
         #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stops play mode in Editor
        #else
        Application.Quit(); // Quits in a built game
        #endif
    }

    
}
