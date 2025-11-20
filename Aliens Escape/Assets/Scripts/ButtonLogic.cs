using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonLogic : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private Slider loadingSlider;
    private GlobalVariables globalVariables;
    public GameObject difficultLevelMenu;
    void Start()
    {
         globalVariables = FindObjectOfType<GlobalVariables>();
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(int SceneIndex)
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
        SceneManager.LoadScene(SceneIndex);
    }

    public void OpenDifficultyMenu()
    {
        difficultLevelMenu.SetActive(true);
    }

    public void ChooseDifficulty(float difficultyLevel)
    {
        difficultLevelMenu.SetActive(false);
        globalVariables.difficultyLevel = difficultyLevel;
        globalVariables.TimeTaken = 0.00f;

        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(1));
        
    }

    IEnumerator LoadLevelASync(int levelToLoad)
{
    AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

    while (!loadOperation.isDone)
    {
        float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
        loadingSlider.value = progressValue;
        yield return null;
    }

    // Subscribe to sceneLoaded to start the timer after the scene is ready

}


}
