using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Assign This to the Rooms PuzzleObjeccts empty Object
// This script is intended to handle interactions with puzzle objects in a room.

public class Puzzleobjectinteraction : MonoBehaviour
{


    public GameObject RoomDoor;
    public GameObject LeversHolder;
    public GameObject ButtonsHolder;
    public GameObject PressureHolder;
    public GameObject RoomControlPanel;


    public Material powered;
    public Material unpowered;






    public GameObject[] roomLevers;
    public GameObject[] roomButtons;
    public GameObject[] roomPressure;

    public GameObject[] roomControlPanels;

    private GameObject wiring;

    bool allPowered;

    private bool continueChecking = true;





    // Start is called before the first frame update
    void Start()
    {


        roomLevers = new GameObject[LeversHolder.transform.childCount];
        for (int i = 0; i < LeversHolder.transform.childCount; i++)
        {
            roomLevers[i] = LeversHolder.transform.GetChild(i).gameObject;
        }

        roomButtons = new GameObject[ButtonsHolder.transform.childCount];
        for (int i = 0; i < ButtonsHolder.transform.childCount; i++)
        {
            roomButtons[i] = ButtonsHolder.transform.GetChild(i).gameObject;
        }

        roomPressure = new GameObject[PressureHolder.transform.childCount];
        for (int i = 0; i < PressureHolder.transform.childCount; i++)
        {
            roomPressure[i] = PressureHolder.transform.GetChild(i).gameObject;
        }



        roomControlPanels = new GameObject[roomLevers.Length + roomButtons.Length + roomPressure.Length];

        int index = 0;

        // Add ActivationPanels from roomLevers
        for (int i = 0; i < roomLevers.Length; i++)
        {
            roomControlPanels[index++] = roomLevers[i].transform.Find("ActivationPanel").gameObject;
        }

        // Add ActivationPanels from roomButtons
        for (int i = 0; i < roomButtons.Length; i++)
        {
            roomControlPanels[index++] = roomButtons[i].transform.Find("ActivationPanel").gameObject;
        }

        // Add ActivationPanels from roomPressure
        for (int i = 0; i < roomPressure.Length; i++)
        {
            roomControlPanels[index++] = roomPressure[i].transform.Find("ActivationPanel").gameObject;
        }



    }


    public void LevelToggle(GameObject lever, Material wireMaterial, Material wireOffMaterial, GameObject Panel, bool isOn, bool autoTurnOff)

    {
        if (!isOn)
        {

            lever.transform.Find("LeverOff").gameObject.SetActive(false);
            lever.transform.Find("LeverOn").gameObject.SetActive(true);
            StartCoroutine(ActivateWires(lever.transform.Find("Wiring").gameObject, wireMaterial, Panel, 0));

        }

        else
        {
            lever.transform.Find("LeverOff").gameObject.SetActive(true);
            lever.transform.Find("LeverOn").gameObject.SetActive(false);
            StartCoroutine(ActivateWires(lever.transform.Find("Wiring").gameObject, wireOffMaterial, Panel, 0));
        }

    }




    public void ButtonToggle(GameObject button, Material wireMaterial, Material wireOffMaterial, GameObject Panel, bool autoTurnOff)

    {

        button.transform.Find("ButtonOff").gameObject.SetActive(false);
        button.transform.Find("ButtonOn").gameObject.SetActive(true);

        StartCoroutine(ActivateWires(button.transform.Find("Wiring").gameObject, wireMaterial, Panel, 0));
        StartCoroutine(ActivateWires(button.transform.Find("Wiring").gameObject, wireOffMaterial, Panel, 4));
        StartCoroutine(AutoTurnOff(2, button.transform.Find("ButtonOn").gameObject, button.transform.Find("ButtonOff").gameObject));
        



    }




    public IEnumerator ActivateWires(GameObject Wires, Material wireMaterial, GameObject Panel, int delay)
    {

        // Iterate through each child of the Wires GameObject
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < Wires.transform.childCount; i++)
        {
            yield return Timer(0.5f);
            GameObject wire = Wires.transform.GetChild(i).gameObject;
            if (wire.GetComponent<MeshRenderer>() != null)
            {
                wire.GetComponent<MeshRenderer>().material = wireMaterial;
            }
        }
        Panel.GetComponent<Renderer>().material = wireMaterial;
        Panel.GetComponent<PanelValues>().isActive = !Panel.GetComponent<PanelValues>().isActive;
        DetectActivePanels();


    }


    IEnumerator AutoTurnOff(float time, GameObject OnState, GameObject offState)
    {
        yield return new WaitForSeconds(time);
        OnState.SetActive(false);
        offState.SetActive(true);
        // Code to execute after the delay
    }


    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
    }

    private void DetectActivePanels()
    {

        for (int i = 0; i < roomControlPanels.Length; i++)
        {
            if (roomControlPanels[i] != null && roomControlPanels[i].GetComponent<Renderer>() != null && continueChecking)
            {
                if (roomControlPanels[i].GetComponent<PanelValues>().isActive == false)
                {
                    allPowered = false;
                    continueChecking = false; // If any panel is inactive, set continueChecking to false
                }


                if (roomControlPanels[i].GetComponent<PanelValues>().isActive == true && i == roomControlPanels.Length - 1)
                {
                    allPowered = true; // If all panels are active, set allPowered to true
                    Debug.Log("All panels checked. All powered: " + allPowered);
                }
                else
                {
                    
                    allPowered = false; // If any panel is inactive, set allPowered to false
                    
                }











            }


        }
        
        continueChecking = true; // Reset continueChecking for the next update
    
       
    }

    public void Update()
    {


        if (allPowered)
        {
            Debug.Log("All activation panels are powered on.");

            for (int i = 0; i < gameObject.transform.Find("ControlPanel").gameObject.transform.Find("Wiring").gameObject.transform.childCount; i++)
            {
                gameObject.transform.Find("ControlPanel").gameObject.transform.Find("Wiring").gameObject.transform.GetChild(i).GetComponent<Renderer>().material = powered;
            }
            RoomDoor.SetActive(false);
            // All activation panels are powered on
            // Place your logic here (e.g., open the door)
        }
        else
        {

            for (int i = 0; i < gameObject.transform.Find("ControlPanel").gameObject.transform.Find("Wiring").gameObject.transform.childCount; i++)
            {
                gameObject.transform.Find("ControlPanel").gameObject.transform.Find("Wiring").gameObject.transform.GetChild(i).GetComponent<Renderer>().material = unpowered;
            }
                  RoomDoor.SetActive(true);
            // Not all activation panels are powered on
            // Place your logic here (e.g., keep the door closed)
        }
    }





}
