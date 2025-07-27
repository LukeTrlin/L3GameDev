using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsToggle : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject optionsPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    
       
    public void ToggleOptionsPanel(GameObject optionsPanelInstance)
    {
        optionsPanelInstance.SetActive(!optionsPanelInstance.activeSelf);
    }
}
