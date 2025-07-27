using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerFunctions : MonoBehaviour
{
    public GameObject OptionsPanelPrefab; // Reference to the OptionsPanel prefab
    private GameObject optionsPanelInstance; // Instance of the OptionsPanel
    private GlobalVariables globalVariables;

    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();

        // Instantiate the OptionsPanel prefab and deactivate it initially
        if (OptionsPanelPrefab != null)
        {
            optionsPanelInstance = Instantiate(OptionsPanelPrefab);
       
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(globalVariables.reload))
        {
            SceneManager.LoadScene(1); // Reload the current scene
        }

        if (Input.GetKeyDown(globalVariables.pause) && optionsPanelInstance != null)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            optionsPanelInstance.SetActive(true); // Toggle the options panel visibility
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Block")
        {
            Rigidbody block = collision.gameObject.GetComponent<Rigidbody>();
            if (block != null && collision.contacts.Length > 0)
            {
                // Calculate push direction based on collision normal
                Vector3 pushDirection = new Vector3(-collision.contacts[0].normal.x, 0, 0);
                block.velocity = pushDirection * 5f; // Adjust speed as needed
            }
       
      
        }

    }

}
