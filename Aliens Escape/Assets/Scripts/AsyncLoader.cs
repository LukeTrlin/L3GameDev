using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private Slider loadingSlider;
    // Start is called before the first frame update



    public void LoadLevelButton(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }


    IEnumerator LoadLevelASync(string levelToLoad)
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
