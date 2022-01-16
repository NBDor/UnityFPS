using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    private int countInside;
    private bool isAlreadyOpen;
    private bool isAlreadyClosed;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Allies" || other.tag.Contains("Enemy") || other.tag == "Player")
        {
            countInside++;
            HandleAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Allies" || other.tag.Contains("Enemy") || other.tag == "Player")
        {
            countInside--;
            HandleAnimation();
        }
    }

    private void HandleAnimation()
    {
        //Debug.Log(countInside);
        // If atleast one person is inside collider and door isn't already open
        if (countInside != 0 && !isAlreadyOpen)
        {
            animator.SetBool("opening", true);
            isAlreadyOpen = true;
        }

        if (countInside <= 0)
        {
            animator.SetBool("opening", false);
            countInside = 0; // In case of unexpected behavior if multiple exits happen at the same time
            isAlreadyOpen = false;
        }
        

    }
}
