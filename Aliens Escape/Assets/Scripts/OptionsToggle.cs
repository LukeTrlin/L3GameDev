using UnityEngine;

public class OptionsToggle : MonoBehaviour
{
    public GameObject optionsMenuInstance;
    private GlobalVariables globalVariables;
    
    
    private void Start()
    {
         globalVariables = FindObjectOfType<GlobalVariables>();
    }
    // Call this function to open the OptionsMenu
    public void OpenOptionsMenu()
    {
        if (optionsMenuInstance != null)
        {
            globalVariables.isOptionsOpen = true;
            optionsMenuInstance.SetActive(true);
        }
    }
}