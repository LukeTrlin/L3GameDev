using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonLogic : MonoBehaviour
{

    private GlobalVariables globalVariables;

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
        SceneManager.LoadScene(SceneIndex);
        globalVariables.TimeTaken = 0.00f;

    }


}
