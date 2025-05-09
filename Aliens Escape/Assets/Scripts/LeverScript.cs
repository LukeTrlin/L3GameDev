using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject leverOff;
    public GameObject leverOn;
    public GameObject wire;
    public GameObject panel;

    public Material wireMaterialOn;
    public Material wireMaterialOff;

    private Renderer[] wireRenderers;
    private bool[] wireStates; // true = on, false = off
    private bool leverToggled = false;
    private Coroutine wireCoroutine;
    private bool animationInProgress = false;

    void Start()
    {
        int count = wire.transform.childCount;
        wireRenderers = new Renderer[count];
        wireStates = new bool[count];

        for (int i = 0; i < count; i++)
        {
            wireRenderers[i] = wire.transform.GetChild(i).GetComponent<Renderer>();
            wireRenderers[i].material = wireMaterialOff;
            wireStates[i] = false;
        }

        panel.GetComponent<Renderer>().material.color = wireMaterialOff.color;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            leverToggled = !leverToggled;

            leverOff.SetActive(!leverToggled);
            leverOn.SetActive(leverToggled);

            

            if (!animationInProgress)
            {
                wireCoroutine = StartCoroutine(AnimateWires());
            }
        }
    }

    IEnumerator AnimateWires()
    {
        animationInProgress = true;

        for (int i = 0; i < wireRenderers.Length; i++)
        {
            // If lever toggled mid-way, keep using current value of leverToggled
            bool desiredState = leverToggled;

            wireRenderers[i].material = desiredState ? wireMaterialOn : wireMaterialOff;
            wireStates[i] = desiredState;

            yield return new WaitForSeconds(0.2f);

            // Check if player toggled lever mid-animation
            if (leverToggled != desiredState)
            {
                // Restart animation from the beginning with the new toggle state
                StartCoroutine(AnimateWires());
                yield break;
            }
        }

        panel.GetComponent<Renderer>().material.color = leverToggled ? wireMaterialOn.color : wireMaterialOff.color;

        animationInProgress = false;
    }
}

