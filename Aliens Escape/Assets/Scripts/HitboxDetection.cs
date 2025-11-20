using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDetection : MonoBehaviour
{
    public MusicController musicController; // Cached MusicController instance
    public GameObject RoomManager; // Reference to the RoomManager GameObject that contains the Puzzleobjectinteraction component

    public GameObject Panel; // Panel to be activated or deactivated

    private Puzzleobjectinteraction puzzleObjectInteraction; // Script to reference the Puzzleobjectinteraction component

    public bool isActiveByDefault = false; // State to track if the panel is currently active

    private bool ActivationCalled = false; // Determine if the player is within the trigger area

    private int DebounceTime; // Time in seconds to debounce the activation

    private bool isDebouncing = false; // Detect if it is on cooldown

    public bool isLever = false; // Determine if the object is a lever
    public bool isButton = false; // Determine if the object is a button
    public bool isPressurePlate = false; // Determine if the object is a pressure plate

    public bool autoTurnnOff;

    public bool isRequired;

    private bool wasHitByBullet = false;


    public Material powerOnMaterial; // Material to apply when the panel is activated
    public Material powerOffMaterial; // Material to apply when the panel is deactivated
    private GlobalVariables globalVariables;

    public int ActivationTime;

    private void Start()
    {
         musicController = FindObjectOfType<MusicController>();
        if (musicController == null)
        {
      
        }
         
        globalVariables = FindObjectOfType<GlobalVariables>();
        if (RoomManager != null)
        {
            puzzleObjectInteraction = RoomManager.GetComponent<Puzzleobjectinteraction>(); // Locate the Puzzleobjectinteraction component in the RoomManager GameObject
          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivationCalled = true;
        }

          if (other.gameObject.CompareTag("bullet"))
        {
            wasHitByBullet = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivationCalled = false;
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(globalVariables.Interact) && ActivationCalled && !isDebouncing && isLever)

        {
        
            musicController.PlayInteractSound();
            
                
            DebounceTime = 1; // Set debounce time in seconds
            isDebouncing = true;
            StartCoroutine(DebounceActivation(DebounceTime));
            puzzleObjectInteraction.LevelToggle(gameObject, powerOnMaterial, powerOffMaterial, Panel, isActiveByDefault, autoTurnnOff);
            isActiveByDefault = !isActiveByDefault; // Toggle the state

        }
        // Also activate if hit by a bullet
        if ((Input.GetKeyDown(globalVariables.Interact) && ActivationCalled && !isDebouncing && isButton) ||
            (isButton && wasHitByBullet && !isDebouncing))
        {
            musicController.PlayInteractSound();
            DebounceTime = 6; // Set debounce time in seconds
            isDebouncing = true;
            StartCoroutine(DebounceActivation(DebounceTime));
            puzzleObjectInteraction.ButtonToggle(gameObject, powerOnMaterial, powerOffMaterial, Panel, autoTurnnOff, ActivationTime);
            wasHitByBullet = false; // Reset after activation
        }
            
            

        

    }


 

    IEnumerator DebounceActivation(int DebounceTime) // Cooldown coroutine to prevent rapid activation
    {
        yield return new WaitForSeconds(DebounceTime);
        isDebouncing = false;
    }
    

}
