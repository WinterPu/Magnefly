using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public GameObject lightRed;
    public GameObject lightGreen;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        print(Global.CountOnTheElevator);

        if (Global.CountOnTheElevator >= 2)
        {
            // Win the level. 
            // Turn the green light on.
            lightGreen.SetActive(true);

            // Move up.
            animator.SetTrigger("Win");
        }
    }

    IEnumerator MoveUpCoroutine()
    {
        yield return new WaitForSeconds(1);

    }
}
