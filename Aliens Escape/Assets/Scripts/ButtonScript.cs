using System.Collections;
using UnityEngine;


// When the wires start turning off, they will also start turning off all other instances of powers wires about to come through

public class LeverScript : MonoBehaviour
{
    public GameObject leverOff;
    public GameObject leverOn;
    public GameObject wire;
    public GameObject panel;

    public Material wireMaterialOn;
    public Material wireMaterialOff;

    private Renderer[] wireRenderers;
    private bool[] wireStates; // true = on, false = off
    private bool isTurningOff = false;

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
            

      
           
                StartCoroutine(AnimateWires());
            
        }
    }

    IEnumerator AnimateWires()
    {
        leverOff.SetActive(false);
        leverOn.SetActive(true);
       

        for (int i = 0; i < wireRenderers.Length; i++)
        {
            wire.transform.GetChild(i).GetComponent<Renderer>().material = wireMaterialOn;
            yield return new WaitForSeconds(0.5f);
            if (isTurningOff == false)
            {
                StartCoroutine(AnimateWiresOff());
            }
            
           
        }

         panel.GetComponent<Renderer>().material.color = wireMaterialOn.color;
             
    }



    IEnumerator AnimateWiresOff()
    {
        isTurningOff = true;
        yield return new WaitForSeconds(3f);
        leverOff.SetActive(true);
        leverOn.SetActive(false);

        for (int i = 0; i < wireRenderers.Length; i++)
        {
             
            wire.transform.GetChild(i).GetComponent<Renderer>().material = wireMaterialOff;
            yield return new WaitForSeconds(0.5f);
            isTurningOff = false;
             
        }
        
        panel.GetComponent<Renderer>().material.color = wireMaterialOff.color;
    }
}
