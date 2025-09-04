using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private bool hasPlayed = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (!hasPlayed)
        {
            StartCoroutine(waiter());
            hasPlayed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        // Continue with the end screen animation
        animator.Play("EndScreenAnimation");
    }
}
